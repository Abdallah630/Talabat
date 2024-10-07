using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.Core.Specification
{
	public class ProductWithBrandAndCategorySpecification : BaseSpecification<Products>
	{
        public ProductWithBrandAndCategorySpecification()
            : base()
        {
            Include.Add(p =>p.Brands);
            Include.Add(p =>p.Categories);
        }
        public ProductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id == id)
        {
            Include.Add(p => p.Brands);
            Include.Add(p => p.Categories);
        }
    }
}
