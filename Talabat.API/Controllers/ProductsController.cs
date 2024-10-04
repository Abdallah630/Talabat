using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.ProductModule;
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
		public async Task<ActionResult<Products>> GetById(int id)
		{
			var product = await _proRepo.GetAsync(id);
			return Ok(product);
		}
	}
}
