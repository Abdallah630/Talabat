using Microsoft.AspNetCore.Mvc;
using Talabat.API.Error;
using Talabat.API.Helpers;
using Talabat.Core.Generic.Contract;
using Talabat.Repository;

namespace Talabat.API.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfile));
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var error = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
																									.SelectMany(p => p.Value.Errors)
																									.Select(p => p.ErrorMessage)
																									.ToList();
					var response = new ApiValidationResponse()
					{
						Errors = error
					};

					return new BadRequestObjectResult(response);
				};
			});
			return services;
		}
	}
}
