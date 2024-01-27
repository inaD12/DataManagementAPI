using DataManagement.API.BackgoundServices;
using DataManagement.API.Extensions;
using DataManagement.API.Middlewares;
using DataManagement.Application;
using DataManagement.Domain;
using DataManagement.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddOptions();

builder.Services.ConfigureAppSettings(config);
builder.Services.InjectAuthentication(config);
builder.Services.AddPolicy();

builder.Services.AddHostedService<StartupService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();


builder.Services
	.AddDomainLayer()
	.AddApplicationLayer()
	.AddInfrastructureLayer();


builder.Host.ConfigureSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<DataCaptureMiddleware>();

app.MapControllers();

app.Run();
