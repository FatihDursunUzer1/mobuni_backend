using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MobUni.ApplicationCore;
using MobUni.ApplicationCore.Interfaces;
using MobUni.ApplicationCore.Interfaces.Repositories;
using MobUni.ApplicationCore.Interfaces.Services;
using MobUni.ApplicationCore.Services;
using MobUni.Infrastructure.Data.Contexts;
using MobUni.Infrastructure.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();

//builder.Services.AddDbContext<MobUniDbContext>(options=> options.UseSqlServer("Data Source=mobuni.c9uwcgm4xelz.us-east-2.rds.amazonaws.com,1433;Initial Catalog=MobUni;User ID=admin;Password=oz15ar47uz28;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
builder.Services.AddDbContext<MobUniDbContext>();
builder.Services.AddTransient<IActivityService, ActivityService>();
builder.Services.AddTransient<IActivityRepository, ActivityRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDepartmentRepository,DepartmentRepository>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
