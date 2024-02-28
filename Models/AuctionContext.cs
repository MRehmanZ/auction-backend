using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace AuctionBackend.Models
{
    public class AuctionBackend : IdentityDbContext<IdentityUser>
    {
        public AuctionBackend(DbContextOptions<AuctionBackend> options) : base(options)
        { }

        public DbSet<AuctionRecord> AuctionRecords { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Item> Items { get; set; }          


    }
}