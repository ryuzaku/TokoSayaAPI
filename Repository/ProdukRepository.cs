using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class ProdukRepository : IProdukRepository
    {
        private readonly TokoSayaContext _context;

        public ProdukRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public bool CreateProduk(Produk produk)
        {
            _context.Add(produk);
            return Save();
        }

        public bool DeleteProduk(int id)
        {
            var produk = GetProduk(id);
            _context.Remove(produk);
            return Save();
        }

        public ICollection<Produk> GetProduk()
        {
            return _context.Produk.OrderBy(p => p.Id).ToList();
        }

        public Produk GetProduk(int id)
        {
            return _context.Produk.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool ProdukExist(int id)
        {
            return _context.Produk.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduk(Produk produk)
        {
            _context.Update(produk);
            return Save();
        }
    }
}
