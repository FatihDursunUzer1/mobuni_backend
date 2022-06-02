using AutoMapper;
using Azure.Storage.Blobs;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobUni.ApplicationCore;
using MobUni.ApplicationCore.Authorization;
using MobUni.ApplicationCore.Errors;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Services;
using MobUni.ApplicationCore.Validation;
using MobUni.Infrastructure.Data.Contexts;
using MobUni.Infrastructure.Repositories;
using MobUni.Infrastructure.Storage;
using MobUni.Infrastructure.UnitOfWork;
using MobUni.WebAPI;
using Google.Cloud.Firestore;

using MobUni.WebAPI.BackgroundServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using MobUni.Infrastructure.Firestore.Users;
using MobUni.Infrastructure.Notification;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddMvc().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<ActivityValidator>();
});
builder.Services.AddControllers(options => options.Filters.Add<ActionFilter>());

//builder.Services.AddDbContext<MobUniDbContext>(options=> options.UseSqlServer("Data Source=mobuni.c9uwcgm4xelz.us-east-2.rds.amazonaws.com,1433;Initial Catalog=MobUni;User ID=admin;Password=oz15ar47uz28;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

builder.Services.AddSingleton<IStorage, AzureStorage >();
builder.Services.AddHostedService<ActivityTimeOutService>();
builder.Services.AddDbContext<MobUniDbContext>(optionsBuilder=>optionsBuilder.UseLazyLoadingProxies().
            UseSqlServer("Data Source=mobuni.c9uwcgm4xelz.us-east-2.rds.amazonaws.com,1433;Initial Catalog=MobUni;User ID=admin;Password=oz15ar47uz28;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"),ServiceLifetime.Transient,ServiceLifetime.Transient);
builder.Services.AddTransient<IActivityService, ActivityService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IQuestionRepository,QuestionRepository>();
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDepartmentRepository,DepartmentRepository>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IUniversityRepository, UniversityRepository>();
builder.Services.AddTransient<IUniversityService,UniversityService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<ILikeQuestionRepository, LikeQuestionRepository>();
builder.Services.AddTransient<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddTransient<IQuestionCommentRepository, QuestionCommentRepository>();
builder.Services.AddTransient<IQuestionCommentService,QuestionCommentService>();
builder.Services.AddTransient<ILikeQuestionRepository,LikeQuestionRepository>();
builder.Services.AddTransient<ILikeService,LikeService>();
builder.Services.AddTransient<IActivityCategoryRepository,ActivityCategoryRepository>();
builder.Services.AddTransient<IActivityCategoryService,ActivityCategoryService>();
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient<IActivityParticipantRepository,ActivityParticipantRepository>();
builder.Services.AddTransient<IActivityParticipantService,ActivityParticipantService>();
builder.Services.AddTransient<IFirestoreUser, FirestoreUser>();
builder.Services.AddTransient<IPushNotification,OneSignalNotification >();



builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problems = new CustomBadRequest(context);

        return new UnprocessableEntityObjectResult(problems);
    };
}).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ActivityValidator>());

/*
 * Json Web Token Authentication Scheme
 * 
 * 
 *  */


System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "mobuniserviceAccountKey.json");
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretJWTkey"]))
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
builder.Services.AddSwaggerGen(opt=>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Lütfen Token giriniz",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthorization();
var app = builder.Build();

//swagger
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
// id control from token
app.UseMiddleware<JwtMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Authorize attribute added to all controllers.
app.MapControllers().RequireAuthorization();

app.Run();
