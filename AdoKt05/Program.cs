using AdoKt05.Data;
using AdoKt05.Data.Seeder;
using AdoKt05.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace AdoKt05;

public abstract class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddAuthorization();

		var corsOrigins = builder.Configuration.GetSection("CorsOrigins").Get<string[]>() ??
		                  throw new InvalidOperationException("String 'CorsOrigins' not found.");
		builder.Services.AddCors(options =>
		{
			options.AddDefaultPolicy(policy =>
			{
				policy.WithOrigins(corsOrigins)
					.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowCredentials();
			});
		});

		builder.Services.AddOpenApi();

		builder.Services.AddDbContext<AppDbContext>(options =>
		{
			options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext") ??
			                  throw new InvalidOperationException("Connection string 'AppDbContext' not found."))
				.UseAsyncSeeding(async (context, _, ct) => { await AppDbContextSeeder.SeedDbAsync(context, ct); });
		});

		builder.Services.AddScoped<IRepository<ProductEntity, Guid>, Repository<ProductEntity, Guid>>();
		builder.Services.AddScoped<IRepository<CategoryEntity, Guid>, Repository<CategoryEntity, Guid>>();
		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

		builder.Services.AddControllers();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();
			app.MapScalarApiReference(); // локалхост/scalar

			using var scope = app.Services.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

			await dbContext.Database.EnsureCreatedAsync();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		await app.RunAsync();
	}
}
