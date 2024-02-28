namespace AuctionBackend.Model
{
    public class Item
    {
        public enum ItemCondition
        {
            NEW,
            EXCELLENT,
            GOOD,
            USED,
            REFURBISHED,
            POOR
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ItemCondition condition { get; set; }
        public string description { get; set; }
        public int DateCreated { get; set; }
        public int  OwnerId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public decimal Price { get; set; }
        public int NumberOfBids { get; set; }
        public bool IsActive { get; set; } 
        public string? WinnerBid { get; set; }













    }
}
