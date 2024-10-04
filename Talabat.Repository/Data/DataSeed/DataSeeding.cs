using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.Repository.Data.DataSeed
{
	public static class DataSeeding
	{
		public async static Task SeedAsync(StoreContext storeContext)
		{
			//Read From brands File 
			var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
			//Convert From json to List<ProductBrand>
			var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
			if (brands?.Count() > 0)
			{
				foreach (var brand in brands)
				{
					storeContext.Set<ProductBrand>().Add(brand);
				}
				await storeContext.SaveChangesAsync(); 
			}

			//Read From categories File 
			var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
			//Convert From Json to List<ProductCategory>
			var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

			if (categories?.Count()>0)
			{
				foreach (var category in categories)
				{
					storeContext.Set<ProductCategory>().Add(category);
				}
				await storeContext.SaveChangesAsync(); 
			}


			//Read From products File
			var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
			//Convert From Json to List<Products>
			var products = JsonSerializer.Deserialize<List<Products>>(productData);
			if (products?.Count()>0)
			{
				foreach (var product in products)
				{
					storeContext.Set<Products>().Add(product);
				}
				await storeContext.SaveChangesAsync(); 
			}
		}
	}
}
