using Microsoft.EntityFrameworkCore;
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

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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

			#region Configure Kestrel MidelleWare
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers(); 
			#endregion

			app.Run();
		}
	}
}
