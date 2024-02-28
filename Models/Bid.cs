namespace AuctionBackend.Model
{
    public class Bid
    {

        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Item { get; set; }
        public string User { get; set; }
        public int DateCreated { get; set; }

        public bool WinningBid { get; set; }
       

    }
}
