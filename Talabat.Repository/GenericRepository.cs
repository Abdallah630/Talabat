using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Generic.Contract;
using Talabat.Core.Modules.ProductModule;
using Talabat.Core.Specification;
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

		public async Task<IEnumerable<T?>> GetAllAsync()
		{
			return await _storeContext.Set<T>().ToListAsync();
		}

		
		public async Task<T?> GetAsync(int id)
		{
			return await _storeContext.FindAsync<T>(id);
		}

		public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetWithSpecAsync(ISpecification<T> specification)
		{
			return await ApplySpecification(specification).FirstOrDefaultAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecification<T> specification)
		{
			return SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), specification);
		}
	}
}
