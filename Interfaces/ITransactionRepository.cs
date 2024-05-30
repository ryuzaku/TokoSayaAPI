using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface ITransactionRepository
    {
        ICollection<Transaction> GetAllTransactions();
        Transaction GetTransaction(int id);
        bool IsTransactionExist(int id);
        float TotalItemPrice(List<Item> items);
        ICollection<Item> GetTransactionItems(int transactionId);
    }
}
