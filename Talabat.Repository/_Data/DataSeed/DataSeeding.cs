using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;
using Talabat.Repository.Data;

namespace Talabat.Repository.Data.DataSeed
{
	public static class DataSeeding
	{
		public async static Task SeedAsync(StoreContext _dbContext)
		{
			if (_dbContext.ProductBrand.Count() == 0)
			{
				//Read From brands File 
				var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				//Convert From json to List<ProductBrand>
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
				if (brands?.Count() > 0)
				{
					foreach (var brand in brands)
					{
						_dbContext.Set<ProductBrand>().Add(brand);
					}
					await _dbContext.SaveChangesAsync();
				}

			}
			if (_dbContext.ProductCategory.Count() == 0)
			{
				//Read From categories File 
				var categoryData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/categories.json");
				//Convert From Json to List<ProductCategory>
				var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

				if (categories?.Count() > 0)
				{
					foreach (var category in categories)
					{
						_dbContext.Set<ProductCategory>().Add(category);
					}
					await _dbContext.SaveChangesAsync();
				}
			}
			if (_dbContext.Products.Count() == 0)
			{
				//Read From products File
				var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				//Convert From Json to List<Products>
				var products = JsonSerializer.Deserialize<List<Products>>(productData);
				if (products?.Count() > 0)
				{
					foreach (var product in products)
					{
						_dbContext.Set<Products>().Add(product);
					}
					await _dbContext.SaveChangesAsync();
				}
			}
		}
	}
}
