namespace TokoSayaAPI.Models
{
    public class Register
    {
        public int Id { get; set; }

        // Nav Prop
        public Toko Toko { get; set; }
        public Penjualan Penjualan { get; set; }
    }
}
