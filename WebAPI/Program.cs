using Application.Interfaces;
using Application.Mappings;
using Application.Models;
using Application.Services;
using Application.Validators;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// NLog: Setup NLog for Dependency injection
//builder.Logging.ClearProviders();
//builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddCors();

builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IDiskRepository, DiskRepository>();
builder.Services.AddScoped<IDiskService, DiskService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

builder.Services.AddSingleton(AutoMapperConfig.Initialize());

builder.Services.AddScoped<IValidator<Query>,MovieQueryValidator>();

builder.Services.AddDbContext<MyMoviesContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyMoviesCS")));


builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyMovies API", Version = "v1" });
});

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
 
app.UseAuthorization();

app.MapControllers();

app.Run();
