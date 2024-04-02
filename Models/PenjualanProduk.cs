namespace TokoSayaAPI.Models
{
    public class PenjualanProduk
    {
        public int PenjualanId { get; set; }
        public int ProdukId { get; set; }
        public int Kuantitas { get; set; }

        // Nav Prop
        public Produk Produk { get; set; }
        public Penjualan Penjualan { get; set; }
    }
}
