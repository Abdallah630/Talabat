using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.API.Error;
using Talabat.API.Extensions;
using Talabat.API.Helpers;
using Talabat.API.Middlewares;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Identity;
using Talabat.Repository._Identity;
using Talabat.Repository._Identity.DataSeed;
using Talabat.Repository.BasketRepo;
using Talabat.Repository.Data;
using Talabat.Repository.Data.DataSeed;
using Talabat.Repository.GenericRepo;

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
			builder.Services.AddDbContext<ApplicationIdentityDbContext>(option =>
			{
				option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddScoped<IConnectionMultiplexer>(serviceProvider =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return ConnectionMultiplexer.Connect(connection);
			});
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{

			}).AddEntityFrameworkStores<ApplicationIdentityDbContext>();
			builder.Services.AddApplicationServices();

			builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
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
			#endregion

			var app = builder.Build();

			#region Update Database
			//Create Scope
			using var scope = app.Services.CreateScope();
			//Scope الموجوده جوه ال Service من Object بنستخدمها علشان نعمل
			var service = scope.ServiceProvider;
			// Ask CLR for Creating Object from DbContext Explicitly
			var _dbContext = service.GetRequiredService<StoreContext>();
			var _identityDbContext = service.GetRequiredService<ApplicationIdentityDbContext>();
			var loggerFactory = service.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbContext.Database.MigrateAsync();
				await DataSeeding.SeedAsync(_dbContext);
				await _identityDbContext.Database.MigrateAsync();
				var _userManger = service.GetRequiredService<UserManager<ApplicationUser>>();
				await ApplicationIdentityDataSeeding.SeedUserAsync(_userManger);

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
