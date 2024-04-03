using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IRegisterRepository
    {
        Penjualan MakePenjualan();
        bool AddItem(PenjualanProduk item);
        bool DeleteItem(PenjualanProduk item);
        bool Save();
    }
}
