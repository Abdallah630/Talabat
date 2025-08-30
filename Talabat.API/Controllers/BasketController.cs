using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.BasketModule;

namespace Talabat.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BasketController : ControllerBase
	{
		private readonly IBasketRepository _basketRepository;
		public BasketController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}
		 

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
		{
			var basket = await _basketRepository.GetBasketAsync(id);
			return Ok(basket is null ? new CustomerBasket(id) :basket);
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UppdateBasket(CustomerBasket basket)
		{
			var createOrUpdatedBaske = await _basketRepository.UpdateBasketAsync(basket);
			if (createOrUpdatedBaske is null) return BadRequest();
			return Ok(createOrUpdatedBaske);
		}

		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string id)
		{
			return await _basketRepository.DeleteBasketAsync(id);
		}
	}
}
