using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AuctionBackend.Models;

namespace AuctionBackend.Models
{
    public class Bid
    {
        public Guid BidId { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key relationship with Auction
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool WinningBid { get; set; } = false;

        // Collection navigation property for BidRecords
        public virtual ICollection<AuctionRecord> AuctionRecords { get; set; }
    }
}
