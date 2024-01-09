using DataManagement.Application.Settings.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;
using System.Text;

namespace DataManagement.API.Extensions
{
	public static class DataInjection
	{
		public static IServiceCollection InjectAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opts =>
				{
					byte[] signingKeyBytes = Encoding.UTF8
						.GetBytes(configuration["Jwtoptions:SigningKey"]);

					opts.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["Jwtoptions:Issuer"],
						ValidAudience = configuration["Jwtoptions:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwtoptions:SigningKey"]))
					};
				});

			services.AddAuthorization();

			return services;
		}

		public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JwtOptionsConfiguration>	
				(configuration.GetSection("Jwtoptions"));


			return services;
		}

		public static void ConfigureSerilog(this IHostBuilder hostBuilder)
		{
			hostBuilder.UseSerilog((context, configuration) =>
				configuration
					.ReadFrom.Configuration(context.Configuration)
					.Enrich.FromLogContext()
			);
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(options =>
			{
				var securityScheme = new OpenApiSecurityScheme
				{
					Name = "JWT Authentication",
					Description = "Enter JWT Bearer token **_only_**",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					BearerFormat = "JWT",
					Reference = new OpenApiReference
					{
						Id = JwtBearerDefaults.AuthenticationScheme,
						Type = ReferenceType.SecurityScheme
					}
				};
				options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{securityScheme, new string[] { }}
			});
			});

			return services;
		}

		public static IServiceCollection AddPolicy(this IServiceCollection services)
		{
			services.AddAuthorization(options =>
			{
				options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
			});

			return services;
		}
	}
}
