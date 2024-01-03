using DataManagement.API.Extensions;
using DataManagement.API.Middlewares;
using DataManagement.Application;
using DataManagement.Domain;
using DataManagement.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	.AddDomainLayer()
	.AddApplicationLayer()
	.AddInfrastructureLayer();

builder.Services.InjectAuthentication(config);
builder.Services.ConfigureAppSettings(config);

builder.Host.ConfigureSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
