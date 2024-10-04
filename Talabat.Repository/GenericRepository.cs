using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.ProductModule;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
       private readonly StoreContext _storeContext;

		public GenericRepository(StoreContext storeContext)
		{
			_storeContext = storeContext;
		}

		public Task<IEnumerable<T?>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _storeContext.FindAsync<T>(id);
		}
	}
}
