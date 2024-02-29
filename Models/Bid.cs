using Microsoft.EntityFrameworkCore;

using AuctionBackend.Models;

namespace AuctionBackend.Models
{
    public class Bid
    {
        public Guid Id { get; set; }

        [Precision(18, 2)]
        public decimal Price { get; set; }
        
        public int ItemId { get; set; }
        
        //public int UserId { get; set; }

        public required virtual User User { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public bool WinningBid { get; set; } = false;
    }
}
