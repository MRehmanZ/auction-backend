using System.ComponentModel.DataAnnotations;

namespace AuctionBackend.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public int AuctionId { get; set; }

        [Required]
        public virtual User User { get; set; }

        //[MaxLength(ContentMaxLength)]
        [Required]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
