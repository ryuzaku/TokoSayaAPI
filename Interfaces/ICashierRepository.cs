using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface ICashierRepository
    {
        Transaction MakeTransaction(Transaction transaction, Cashier cashier);
    }
}
