namespace AuctionBackend.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public DateTime DateCreated { get; set; }


    }
}
