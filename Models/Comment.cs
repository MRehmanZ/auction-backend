using System.ComponentModel.DataAnnotations;

namespace AuctionBackend.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }

        //[MaxLength(ContentMaxLength)]
        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        // Foreign key relationship with ApplicationUser
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        // Foreign key relationship with Auction
        public Guid AuctionId { get; set; }
        public Auction Auction { get; set; }
    }
}
