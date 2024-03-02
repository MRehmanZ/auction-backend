using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using AuctionBackend.Models;

namespace AuctionBackend.Models
{
    public class Bid
    {
        public Guid Id { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        
        public int ItemId { get; set; }

        //public Guid UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool WinningBid { get; set; } = false;
    }
}
