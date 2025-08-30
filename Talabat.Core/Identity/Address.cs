namespace Talabat.Core.Identity
{
	public class Address
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string lastName { get; set; }
		public string street { get; set; }
		public string city { get; set; }
		public string Country { get; set; }
		public string ApplicationUserId { get; set; } // Foreign Key
		public ApplicationUser User { get; set; } // Navigational Property [One]

	}
}