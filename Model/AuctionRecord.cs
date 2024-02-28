namespace AuctionBackend.Model
{
    public class AuctionRecord
    {
        public int UserId { get; set; }
        public int CurrentBid { get; set; }
        public int CurrentHighestBid { get; set; }
        public ICollection<Bid> BidRecords { get; set; }

    }
}
