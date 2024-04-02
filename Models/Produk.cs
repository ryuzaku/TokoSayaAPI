namespace TokoSayaAPI.Models
{
    public class Produk
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public int Harga { get; set; }
        public string ItemId { get; set; }

        // Nav Prop
        public ICollection<PenjualanProduk> PenjualanProduk { get; set; }
    }
}
