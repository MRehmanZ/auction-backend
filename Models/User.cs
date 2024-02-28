using Microsoft.AspNetCore.Identity;

namespace AuctionBackend.Models

{
    public class User : IdentityUser
    {
        public User() 
        {
            this.Id = Guid.NewGuid().ToString();
            this.Bids = new HashSet<Bid>();
        }

        //public Guid Id { get; set; }

        //public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public decimal Balance { get; set; } = decimal.Zero;
        
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
