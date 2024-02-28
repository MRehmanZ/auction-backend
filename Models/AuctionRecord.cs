namespace AuctionBackend.Models
{
    public class AuctionRecord
    {
        public AuctionRecord()
        {
            this.BidRecords = new HashSet<Bid>();
        }
            
        public Guid Id { get; set; }

        public int UserId { get; set; } // TODO: check if UserId or BidId

        public int CurrentBid { get; set; }

        public int CurrentHighestBid { get; set; }

        public ICollection<Bid> BidRecords { get; set; }
    }
}
