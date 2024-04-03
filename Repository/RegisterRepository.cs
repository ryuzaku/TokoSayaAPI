using Microsoft.EntityFrameworkCore;
using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly TokoSayaContext _context;

        public RegisterRepository(TokoSayaContext context)
        {
            _context = context;
        }
        public bool AddItem(PenjualanProduk item)
        {
            _context.Add(item);
            return Save();
        }

        public bool DeleteItem(PenjualanProduk item)
        {
            _context.Remove(item);
            return Save();
        }

        public Penjualan MakePenjualan()
        {
            var penjualan = new Penjualan()
            {
                Tanggal = DateTime.Now
            };

            _context.Add(penjualan);
            _context.SaveChanges();
            return penjualan;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
