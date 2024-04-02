using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IRegisterRepository
    {
        Penjualan MakePenjualan();
        int MakePembayaran(int penjualanId, int nominalDibayarkan, int totalPembayaran);
        bool AddItem(PenjualanProduk item);
        bool EditItemQty(PenjualanProduk penjualanProduk);
        bool DeleteItem(int penjualanId, int produkId);
        bool Save();
    }
}
