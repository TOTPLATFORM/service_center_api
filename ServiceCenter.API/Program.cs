
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceCenter.API.ExceptionHandlers;
using ServiceCenter.API.ExtensionMethods;
using ServiceCenter.Domain.Entities;
using ServiceCenter.Infrastructure.BaseContext;

namespace ServiceCenter.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Host.UseSerilog((context, configuration) =>
		  configuration.ReadFrom.Configuration(context.Configuration));

		builder.Services.AddExceptionHandler<ResourceNotFoundExceptionHandler>();
		builder.Services.AddExceptionHandler<AuthorizationExceptionHandler>();
		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "Enter your token"
			});

			options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Name = "Bearer",
							In = ParameterLocation.Header
						},
						new List<string>()
					}
				});
		});


		builder.Services.AddCenterServices(builder.Configuration);


		builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				   .AddEntityFrameworkStores<ServiceCenterBaseDbContext>();

		builder.Services.AddDbContext<ServiceCenterBaseDbContext>(options =>
				   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionForSql"),
					b => b.MigrationsAssembly("ServiceCenter.Infrastructure.Sql")));

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
	}
}
