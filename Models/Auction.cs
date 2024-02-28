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

        public required string Name { get; set; }
        
        public ItemCondition Condition { get; set; }
        
        public required string Description { get; set; }
        
        public Guid Owner { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime ExpiryDate { get; set; }
        
        public decimal Price { get; set; }
        
        public int NumberOfBids { get; set; } = 0;
        
        public bool IsActive { get; set; } = false;
        
        public int? WinnerBid { get; set; } // BidId

        public virtual ICollection<Bid> Bids { get; set; }
    }
}
