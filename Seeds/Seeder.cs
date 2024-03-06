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

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await CreateUser(userManager, "Admin", "mrz707@outlook.com", "@Hello1234");
                await CreateUser(userManager, "User", "emailtestreceive@gmail.com", "@ABcd1234");
                await CreateCategory(context, "Electronics");
                await CreateCategory(context, "Antiques");
                await CreateCategory(context, "Cars");
                await CreateCategory(context, "Paintings");
                var user = await userManager.FindByEmailAsync("mrz707@outlook.com");
                if (user != null)
                {
                    await CreateAuctionItem(context, user);
                }
            }
        }

        private static async Task CreateUser(UserManager<ApplicationUser> userManager, string role, string sampleEmail, string samplePassword)
        {

            if (await userManager.FindByEmailAsync(sampleEmail) == null)
            {
                var user = new ApplicationUser();
                user.Email = sampleEmail;
                user.UserName = sampleEmail;

                await userManager.CreateAsync(user, samplePassword);

                await userManager.AddToRoleAsync(user, role);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, token);
            }
        }

        private static async Task CreateCategory(AuctionContext context, string sampleName)
        {
            var category = new Category();
            category.Name = sampleName;
            category.Auctions = new List<Auction>();
            await context.SaveChangesAsync();
        }

        private static async Task CreateAuctionItem(AuctionContext context, ApplicationUser user)
        {
            var auctionItem = new Auction();
            auctionItem.Name = "Television";
            auctionItem.Condition = Auction.ItemCondition.REFURBISHED;
            auctionItem.Description = "Recently refurbished, but new condition";
            auctionItem.UserId = user.Id;
            auctionItem.IsActive = true;
            auctionItem.Price = 590;

            var category = context.Categories.FirstOrDefault(c => c.Name == "Electronics");
            await context.SaveChangesAsync();
        }
    }
}