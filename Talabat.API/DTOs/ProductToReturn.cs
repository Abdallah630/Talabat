using Talabat.Core.Modules.ProductModule;

namespace Talabat.API.DTOs
{
	public class ProductToReturn 
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string PictureUrl { get; set; }
		public decimal Price { get; set; }

		public int BrandId { get; set; }
		public string Brands { get; set; }
		public int CategoryId { get; set; }
		public string Categories { get; set; }
	}
}
