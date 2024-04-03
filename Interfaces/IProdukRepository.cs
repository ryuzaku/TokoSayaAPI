using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IProdukRepository
    {
        ICollection<Produk> GetProduk();
        Produk GetProduk(int id);
        bool ProdukExist(int id);
        bool CreateProduk(Produk produk);
        bool UpdateProduk(Produk produk);
        bool DeleteProduk(int id);
        bool Save();
    }
}
