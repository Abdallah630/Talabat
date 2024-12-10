using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Modules.BasketModule
{
	public class CustomerBasket
	{
		public string Id { get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public CustomerBasket(string id, List<BasketItem> basketItems)
		{
			Id = id;
			BasketItems = basketItems;
		}

	}
}
