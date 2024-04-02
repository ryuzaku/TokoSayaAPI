using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface ITokoRepository
    {
        Toko GetToko(int id);
        ICollection<Toko> GetToko();
        bool CreateToko(Toko toko);
        bool UpdateToko(Toko toko);
        bool DeleteToko(Toko toko);
        bool TokoExists(int id);
        bool Save();
    }
}
