using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IPenjualanRepository
    {
        ICollection<Penjualan> GetPenjualan();
        Penjualan GetPenjualan(int id);
        int GetTotal(int penjualanId);
    }
}
