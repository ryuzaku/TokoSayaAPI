namespace TokoSayaAPI.Models
{
    public class Penjualan
    {
        public int Id { get; set; }
        public DateTime Tanggal { get; set; }

        // Nav Prop
        public Register Register { get; set; }
        public ICollection<PenjualanProduk> PenjualanProduk { get; set; }
    }
}
