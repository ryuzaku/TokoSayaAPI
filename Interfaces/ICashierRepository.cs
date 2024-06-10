using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface ICashierRepository
    {
        bool MakeTransaction(Transaction transaction);
    }
}
