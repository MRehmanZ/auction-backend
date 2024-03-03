using System.ComponentModel.DataAnnotations;

namespace AuctionBackend.Models
{
    public class Category
    {
        public Category()
        {
            this.Auctions = new HashSet<Auction>();
        }
        public Guid CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Auction> Auctions { get; set; }
    }
}
