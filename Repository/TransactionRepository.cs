using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TokoSayaContext _context;

        public TransactionRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public ICollection<Transaction> GetAllTransactions()
        {
            return _context.Transactions.OrderBy(t => t.Id).ToList();
        }

        public ICollection<Item> GetTransactionItems(int transactionId)
        {
            return _context.TransactionItems.Where(x => x.TransactionId == transactionId).Select(x => x.Item).ToList();
        }

        public Transaction GetTransaction(int id)
        {
            return _context.Transactions.Where(t => t.Id == id).FirstOrDefault();
        }

        public bool IsTransactionExist(int id)
        {
            return _context.Transactions.Any(t => t.Id == id);
        }

        public float TotalItemPrice(List<Item> items)
        {
            float sum = 0;
            foreach (var item in items)
            {
                sum += item.Price;
            }
            return sum;
        }
    }
}
