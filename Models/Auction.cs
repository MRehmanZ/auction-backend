using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AuctionBackend.Models;
using System.Collections.Generic;

namespace AuctionBackend.Models
{
    public class Auction
    {
        public Auction()
        {
            Bids = new HashSet<Bid>();
        }
        public enum ItemCondition
        {
            NEW,
            EXCELLENT,
            GOOD,
            USED,
            REFURBISHED,
            POOR
        }

        [Key]
        public Guid AuctionId { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ItemCondition Condition { get; set; }

        [Required]
        public string Description { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key relationship with Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime ExpiryDate { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        
        public int NumberOfBids { get; set; } = 0;
        
        public bool IsActive { get; set; } = false;
        
        public int? WinnerBid { get; set; } // BidId

        // Collection navigation property for Auctions created by the user
        public ICollection<AuctionRecord> AuctionRecords { get; set; }

        // Collection navigation property for Bids placed by the user
        public ICollection<Bid> Bids { get; set; }

        // Collection navigation property for Comments posted by the user
        public ICollection<Comment> Comments { get; set; }
    }
}
