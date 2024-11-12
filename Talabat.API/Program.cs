using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Talabat.API.Error;
<<<<<<< HEAD
using Talabat.API.Extensions;
=======
>>>>>>> 9d0da82ca6f7c5293cf22e4a1b987e908e7618ae
using Talabat.API.Helpers;
using Talabat.API.Middlewares;
using Talabat.Core.Generic.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;

namespace Talabat.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Configure Service
			// Add services to the container.

			builder.Services.AddControllers(); //API Services
			
			builder.Services.AddSwaggerServices();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
<<<<<<< HEAD
			builder.Services.AddApplicationServices();
=======

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(e=>e.Value.Errors.Count()>0)
																								.SelectMany(e=>e.Value.Errors)
																								.Select(e=>e.ErrorMessage)
																								.ToList();
					var response = new ApiValidationResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});
>>>>>>> 9d0da82ca6f7c5293cf22e4a1b987e908e7618ae
			#endregion

			var app = builder.Build();

			#region Update Database
			//Create Scope
			using var scope = app.Services.CreateScope();
			//Scope الموجوده جوه ال Service من Object بنستخدمها علشان نعمل
			var service = scope.ServiceProvider;
			// Ask CLR for Creating Object from DbContext Explicitly
			var _dbContext = service.GetRequiredService<StoreContext>();
			var loggerFactory = service.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
				await DataSeeding.SeedAsync(_dbContext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "an error has been occurred during apply migration");
			}

			#endregion

			#region Configure Kestrel Middlewares
			app.UseMiddleware<ExceptionMiddleware>();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleware();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseStaticFiles();

			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
