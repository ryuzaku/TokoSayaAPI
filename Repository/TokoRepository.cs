using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class TokoRepository : ITokoRepository
    {
        private readonly TokoSayaContext _context;

        public TokoRepository(TokoSayaContext context)
        {
            _context = context;
        }
        public bool CreateToko(Toko toko)
        {
            _context.Add(toko);
            return Save();
        }

        public bool DeleteToko(Toko toko)
        {
            _context.Remove(toko);
            return Save();
        }

        public ICollection<Toko> GetToko()
        {
            return _context.Toko.OrderBy(x => x.Id).ToList();
        }

        public Toko GetToko(int id)
        {
            return _context.Toko.Where(t => t.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TokoExists(int id)
        {
            return _context.Toko.Any(t => t.Id == id);
        }

        public bool UpdateToko(Toko toko)
        {
            _context.Update(toko);
            return Save();
        }
    }
}
