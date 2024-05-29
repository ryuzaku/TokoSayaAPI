namespace TokoSayaAPI.Models
{
    public class TransactionItem
    {
        public int TransactionId { get; set; }
        public int ItemId { get; set; }

        // nav prop
        public Transaction Transaction { get; set; }
        public Item Item { get; set; }
    }
}
