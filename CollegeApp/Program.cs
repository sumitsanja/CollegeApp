using CollegeApp.Data;
using CollegeApp.MyLogging;
using Microsoft.Extensions.Options;
using Serilog;
using Microsoft.EntityFrameworkCore;
using CollegeApp.Confuguration;
using AutoMapper;
using CollegeApp.Data.Repository;

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

#region serilog settings
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.File("Log/log.text", rollingInterval: RollingInterval.Minute)
//    .CreateLogger();

//use this line to override the built in loggers
//builder.Host.UseSerilog();

//use serilog along with built in logger    
//builder.Logging.AddSerilog();
#endregion 

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

//for sql server use
builder.Services.AddDbContext<CollegeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});



// Add services to the container.

builder.Services.AddControllers(options=> options.ReturnHttpNotAcceptable=true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



//register automapper

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

builder.Services.AddScoped<IMyLogger, LogToDb>();
builder.Services.AddSingleton<IMyLogger, LogToDb>();
builder.Services.AddTransient<IMyLogger, LogToDb>();

builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));

builder.Services.AddCors(options => options.AddPolicy("mytestcors", policy =>
{
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("mytestcors");

app.UseAuthorization();

app.MapControllers();

app.Run();
