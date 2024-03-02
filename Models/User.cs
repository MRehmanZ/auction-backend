
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Precision(18, 2)]
        public decimal Balance { get; set; } = decimal.Zero;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
