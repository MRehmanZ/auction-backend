using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AuctionBackend.Models;
using System;
using System.Threading.Tasks;

namespace AuctionBackend.Seeds
{
    public static class Seeder
    {

        private static readonly RoleManager<IdentityRole<Guid>> roleManager;
        private static readonly UserManager<ApplicationUser> userManager;

        public static async Task InitializeAsync(IServiceProvider services)
        {
            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                // Check if the Admin role already exists
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // If the roles don't exist, create them
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            // Admin User creation
    

            string email = "emailtestreceive@gmail.com";

            string password = "@Hello1234";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new ApplicationUser();
                user.Email = email;
                user.UserName = email;

                await userManager.CreateAsync(user, password);

                await userManager.AddToRoleAsync(user, "Admin");

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, token);
            }
        }
    }
}