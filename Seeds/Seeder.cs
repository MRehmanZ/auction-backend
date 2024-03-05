using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using AuctionBackend.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuctionBackend.Seeds
{
    public static class Seeder
    {

        public static async Task InitializeAsync(IServiceProvider services)
        {

            using (var context = new AuctionContext(services.GetRequiredService<DbContextOptions<AuctionContext>>()))
            {
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                string[] roles = { "Admin", "User" };

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


                string email = "mhamza3256@gmail.com";

                string password = "@Hello1234";

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

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
}