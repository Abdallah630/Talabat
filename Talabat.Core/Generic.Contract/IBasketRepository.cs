using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.BasketModule;

namespace Talabat.Core.Generic.Contract
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketAsync(string BasketId);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket);
		Task<bool> DeleteBasketAsync(string basketId);
	}
}
