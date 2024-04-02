namespace TokoSayaAPI.Models
{
    public class Toko
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string NoTlp { get; set; }

        // Nav Prop
        public Register Register { get; set; }
    }
}
