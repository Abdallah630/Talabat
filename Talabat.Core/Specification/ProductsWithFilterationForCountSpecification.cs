using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.Core.Specification
{
	public class ProductsWithFilterationForCountSpecification : BaseSpecification<Products>
	{
		public ProductsWithFilterationForCountSpecification(productSpecParams specParams)
			: base( p=>
					(string.IsNullOrEmpty(specParams.search) || p.Name.ToLower().Contains(specParams.search))
					&&
					(!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId)
					&&
					(!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId)
				  )
		{

		}
	}
}
