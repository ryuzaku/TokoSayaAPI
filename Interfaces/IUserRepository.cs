using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        ICollection<User> GetUsers();
        bool IsUserExist(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        bool Save();
    }
}
