using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.API.Helpers
{
	public class PictureResolve : IValueResolver<Products, ProductToReturn, string>
	{
		private readonly IConfiguration _configuration;

		public PictureResolve(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(Products source, ProductToReturn destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
			{
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			}
			return string.Empty;
		}
	}
}
