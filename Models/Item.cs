namespace TokoSayaAPI.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        // nav prop
        public ICollection<TransactionItem> TransactionItems { get; set; }
    }
}
