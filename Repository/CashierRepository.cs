using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class CashierRepository : ICashierRepository
    {
        private readonly TokoSayaContext _context;

        public CashierRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public Transaction MakeTransaction(Transaction transaction, Cashier cashier)
        {
            transaction.Cashier = cashier;
            _context.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }
    }
}
