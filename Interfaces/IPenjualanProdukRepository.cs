using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IPenjualanProdukRepository
    {
        int GetSubtotal(PenjualanProduk item);
    }
}
