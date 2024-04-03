using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class PenjualanProdukRepository : IPenjualanProdukRepository
    {
        private readonly TokoSayaContext _context;

        public PenjualanProdukRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public int GetSubtotal(PenjualanProduk item)
        {
            var p = _context.Produk.Where(p => p.Id == item.ProdukId).FirstOrDefault();
            return item.Kuantitas * p.Harga;
        }
    }
}
