namespace AuctionBackend.Models
{
    public class Comment
    {
        public Guid Id { get; set; }

        public int AuctionId { get; set; }

        public int UserId { get; set; }

        //[MaxLength(ContentMaxLength)]
        public required string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
