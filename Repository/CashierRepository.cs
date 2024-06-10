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

        public bool MakeTransaction(Transaction transaction)
        {
            _context.Add(transaction);
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
