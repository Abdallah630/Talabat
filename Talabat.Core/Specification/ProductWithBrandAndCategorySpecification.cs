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
        public ProductWithBrandAndCategorySpecification(productSpecParams specParams)
            : base(p =>
            (string.IsNullOrEmpty(specParams.search) || p.Name.ToLower().Contains(specParams.search))
            &&
            (!specParams.BrandId.HasValue ||  p.BrandId == specParams.BrandId.Value)
            &&
            (!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)
            )
        {
            Include.Add(p =>p.Brands);
            Include.Add(p =>p.Categories);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch(specParams.Sort)
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

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
            
        }
        public ProductWithBrandAndCategorySpecification(int id)
            :base(p=>p.Id == id)
        {
            Include.Add(p => p.Brands);
            Include.Add(p => p.Categories);
        }
    }
}
