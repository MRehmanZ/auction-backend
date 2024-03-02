using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AuctionBackend.Models;

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

        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public ItemCondition Condition { get; set; }

        [Required]
        public string Description { get; set; }

        //public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime ExpiryDate { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        
        public int NumberOfBids { get; set; } = 0;
        
        public bool IsActive { get; set; } = false;
        
        public int? WinnerBid { get; set; } // BidId

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
