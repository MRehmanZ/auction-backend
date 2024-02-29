
using Microsoft.EntityFrameworkCore;

namespace AuctionBackend.Models

{
    public class User 
    {
        public User() 
        {
            this.Id = Guid.NewGuid();
            this.Bids = new HashSet<Bid>();
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Precision(18, 2)]
        public decimal Balance { get; set; } = decimal.Zero;
        
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
