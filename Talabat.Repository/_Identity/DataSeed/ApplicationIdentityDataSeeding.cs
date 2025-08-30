using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Identity;

namespace Talabat.Repository._Identity.DataSeed
{
	public static class ApplicationIdentityDataSeeding
	{
		public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
		{
			if (!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					DisplayName = "Abdallah Saad",
					Email = "Abdallah@gmail.com",
					UserName = "Aballah.saad",
					PhoneNumber = "0123333749"
				}; 
				await userManager.CreateAsync(user,"P@ssword123");
			}
		}
	}
}
