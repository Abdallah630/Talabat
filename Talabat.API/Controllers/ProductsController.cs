using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Error;
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
		private readonly IGenericRepository<ProductBrand> _proBrand;
		private readonly IGenericRepository<ProductCategory> _proCategory;
		private readonly IMapper _mapper;
		public ProductsController(StoreContext storeContext, IGenericRepository<Products> proRepo, IMapper mapper, IGenericRepository<ProductBrand> proBrand, IGenericRepository<ProductCategory> proCategory)
		{
			_storeContext = storeContext;
			_proRepo = proRepo;
			_mapper = mapper;
			_proBrand = proBrand;
			_proCategory = proCategory;
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturn>>> GetAll(string? sort,int? brandId,int? categoryId)
		{
			var spec = new ProductWithBrandAndCategorySpecification(sort,brandId,categoryId);
			var product = await _proRepo.GetAllWithSpecAsync(spec);
			return Ok(_mapper.Map<IReadOnlyList<Products>,IReadOnlyList<ProductToReturn>>(product));
		}
		//الممكن يرجع Responseتحديد شكل ال 
		[ProducesResponseType(typeof(ProductToReturn),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturn>> GetById(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _proRepo.GetWithSpecAsync(spec);
			if (product is null) return BadRequest();
			return Ok(_mapper.Map<Products,ProductToReturn>(product));
		}
		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrand()
		{
			var brand = await _proBrand.GetAllAsync();
			return Ok(brand);
		}
		[HttpGet("Categories")]
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategory()
		{
			var category = await _proCategory.GetAllAsync();
			return Ok(category);
		}
	}
}
