using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public ProductsController(StoreContext storeContext, IGenericRepository<Products> proRepo)
		{
			_storeContext = storeContext;
			_proRepo = proRepo;
		}

		[HttpGet]
		public async Task<ActionResult<Products>> GetAll()
		{
			var spec = new ProductWithBrandAndCategorySpecification();
			var product = await _proRepo.GetAllWithSpecAsync(spec);
			return Ok(product);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Products>> GetById(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecification(id);
			var product = await _proRepo.GetWithSpecAsync(spec);
			return Ok(product);
		}
	}
}
