using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace AuctionBackend.Models
{
    public class AuctionContext : IdentityDbContext<IdentityUser>
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        public DbSet<AuctionRecord> AuctionRecords { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Auction> Auctions { get; set; }
     
    }
}