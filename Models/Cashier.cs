namespace TokoSayaAPI.Models
{
    public class Cashier
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // nav prop
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
