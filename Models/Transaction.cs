namespace TokoSayaAPI.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public float TotalPrice { get; set; }
        public int CashierId { get; set; }

        // nav prop
        public Cashier Cashier { get; set; }
        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
