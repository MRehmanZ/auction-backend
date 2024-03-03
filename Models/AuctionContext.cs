using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace AuctionBackend.Models
{
    public class AuctionContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        { }


        public DbSet<AuctionRecord> AuctionRecords { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Auction> Auctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring a one-to-many relationship between ApplicationUser and Auction
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Auctions)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId);

            // Configuring a one-to-many relationship between ApplicationUser and Bid
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            // Configuring a one-to-many relationship between Auction and Bid
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionId);

            // Configuring a one-to-many relationship between Bid and AuctionRecord
            modelBuilder.Entity<Bid>()
                .HasMany(b => b.BidRecords)
                .WithOne(ar => ar.Bid)
                .HasForeignKey(ar => ar.BidId);

            // Configuring a one-to-many relationship between Bid and AuctionRecord
            modelBuilder.Entity<Category>()
                .HasMany(a => a.Auctions)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            // Configuring a one-to-many relationship between Bid and AuctionRecord
           modelBuilder.Entity<Auction>()
                .HasMany(a => a.Comments)
                .WithOne(b => b.Auction)
                .HasForeignKey(c => c.AuctionId);

        }

    }
}