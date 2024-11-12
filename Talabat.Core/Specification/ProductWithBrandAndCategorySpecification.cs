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
        public ProductWithBrandAndCategorySpecification(string sort)
            : base()
        {
            Include.Add(p =>p.Brands);
            Include.Add(p =>p.Categories);

            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p=>p.Name);
                        break;

                }
            }else AddOrderBy(p => p.Name);
            
            
        }
        public ProductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id == id)
        {
            Include.Add(p => p.Brands);
            Include.Add(p => p.Categories);
        }
    }
}
