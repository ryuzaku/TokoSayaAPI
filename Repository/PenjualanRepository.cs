using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class PenjualanRepository : IPenjualanRepository
    {
        private readonly TokoSayaContext _context;

        public PenjualanRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public ICollection<Penjualan> GetPenjualan()
        {
            return _context.Penjualan.OrderBy(p => p.Id).ToList();
        }

        public Penjualan GetPenjualan(int id)
        {
            return _context.Penjualan.Where(p => p.Id == id).FirstOrDefault();
        }

        public int GetTotal(int penjualanId)
        {
            var penjualanProduk = _context.PenjualanProduk
                .Where(pp => pp.PenjualanId == penjualanId).ToList();

            var total = 0;
            foreach (var item in penjualanProduk)
            {
                var p = _context.Produk.Where(p => p.Id == item.ProdukId).FirstOrDefault();
                var subtotal = item.Kuantitas * p.Harga;
                total += subtotal;
            }

            return total;
        }
    }
}
