namespace AuctionBackend.Models
{
    public class AuctionRecord
    {
        public AuctionRecord()
        {
            this.BidRecords = new HashSet<Bid>();
        }
            
        public Guid AuctionRecordId { get; set; }
        public DateTime RecordTime { get; set; }
        public string Action { get; set; } // Example: "BidPlaced", "BidUpdated", "BidRemoved"

        public Guid CurrentBid { get; set; }

        public int CurrentHighestBid { get; set; }

        public ICollection<Bid> BidRecords { get; set; }

        // Foreign key relationship with ApplicationUser
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key relationship with Auction
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }

        // Additional properties related to bid information
        public Guid BidId { get; set; }
        public Bid Bid { get; set; }
    }
}
