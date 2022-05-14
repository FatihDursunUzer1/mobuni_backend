using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MobUni.ApplicationCore;
using MobUni.ApplicationCore.Authorization;
using MobUni.ApplicationCore.Errors;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Services;
using MobUni.Infrastructure.Data.Contexts;
using MobUni.Infrastructure.Repositories;
using MobUni.Infrastructure.Storage;
using MobUni.WebAPI;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers(options=>options.Filters.Add<ActionFilter>());

//builder.Services.AddDbContext<MobUniDbContext>(options=> options.UseSqlServer("Data Source=mobuni.c9uwcgm4xelz.us-east-2.rds.amazonaws.com,1433;Initial Catalog=MobUni;User ID=admin;Password=oz15ar47uz28;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

builder.Services.AddSingleton<IStorage, AzureStorage >();
builder.Services.AddDbContext<MobUniDbContext>();
builder.Services.AddTransient<IActivityService, ActivityService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IQuestionRepository,QuestionRepository>();
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDepartmentRepository,DepartmentRepository>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problems = new CustomBadRequest(context);

        return new UnprocessableEntityObjectResult(problems);
    };
});

/*
 * Json Web Token Authentication Scheme
 * 
 * 
 *  */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "mobuni",
            ValidAudience = "mobuni",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MobUniMobilProgramxxxs"))
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    //Unauthorized api 
                    await context.Response.WriteAsync(new Error("Unauthorized").ToString());
                });

                return Task.CompletedTask;
            }
        };
    });
#region
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});


IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
var app = builder.Build();

//swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
// id control from token
app.UseMiddleware<JwtMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Authorize attribute added to all controllers.
app.MapControllers().RequireAuthorization();

app.Run();
