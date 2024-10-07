using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.ProductModule;
using Talabat.Core.Specification;
using Talabat.Repository.Data;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly StoreContext _storeContext;
		private readonly IGenericRepository<Products> _proRepo;
		private readonly IMapper _mapper;
		public ProductsController(StoreContext storeContext, IGenericRepository<Products> proRepo, IMapper mapper)
		{
			_storeContext = storeContext;
			_proRepo = proRepo;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductToReturn>>> GetAll()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var product = await _proRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IEnumerable<Products>,IEnumerable<ProductToReturn>>(product));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturn>> GetById(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _proRepo.GetWithSpecAsync(spec);
			if (product is null) return BadRequest();
			return Ok(_mapper.Map<Products,ProductToReturn>(product));
		}
	}
}
