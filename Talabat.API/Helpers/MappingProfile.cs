using AutoMapper;
using Talabat.API.DTOs;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.API.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Products, ProductToReturn>()
				.ForMember(p => p.Brands, o => o.MapFrom(s => s.Brands.Name))
				.ForMember(p => p.Categories, o => o.MapFrom(s => s.Categories.Name))
				.ForMember(p=>p.PictureUrl,o=>o.MapFrom<PictureResolve>());
		}
	}
}
