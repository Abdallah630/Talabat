using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.Core.Generic.Contract
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<IEnumerable<T?>> GetAllAsync();
		Task<T?> GetAsync(int id); 
	}
}
