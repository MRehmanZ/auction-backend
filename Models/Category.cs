namespace AuctionBackend.Models
{
    public class Category
    {
        public Category()
        {
            this.Auctions = new HashSet<Auction>();
        }
        public Guid Id { get; set; }
       
        public required string Name { get; set; }

        public ICollection<Auction> Auctions { get; set; }
    }
}
