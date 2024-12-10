using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.BasketModule;

namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
	    private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}

		public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
		{
			var basket = await _database.StringGetAsync(BasketId);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(BasketId);
		}


		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket)
		{
			var createOrUpdate = await _database.StringSetAsync(Basket.Id,JsonSerializer.Serialize(Basket),TimeSpan.FromDays(30));
			if (!createOrUpdate) return null;
			return await GetBasketAsync(Basket.Id);
		}
		public async Task<bool> DeleteBasketAsync(string basketId)
		{
			return await _database.KeyDeleteAsync(basketId);
		}
	}
}
