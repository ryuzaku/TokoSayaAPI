using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Item> GetItems();
        Item GetItem(int id);
        bool IsItemExist(int id);
        bool CreateItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(int id);
        bool Save();
    }
}
