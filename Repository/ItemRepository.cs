using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Models;

namespace TokoSayaAPI.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly TokoSayaContext _context;

        public ItemRepository(TokoSayaContext context)
        {
            _context = context;
        }

        public bool CreateItem(Item item)
        {
            _context.Add(item);
            return Save();
        }

        public bool DeleteItem(int id)
        {
            var item = GetItem(id);
            _context.Remove(item);
            return Save();
        }

        public Item GetItem(int id)
        {
            return _context.Items.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICollection<Item> GetItems()
        {
            return _context.Items.OrderBy(x => x.Id).ToList();
        }

        public bool IsItemExist(int id)
        {
            return _context.Items.Any(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateItem(Item item)
        {
            _context.Update(item);
            return Save();
        }
    }
}
