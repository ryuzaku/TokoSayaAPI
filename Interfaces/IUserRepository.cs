using TokoSayaAPI.Models;

namespace TokoSayaAPI.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        bool IsUserExist(int id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int id);
        bool Save();
    }
}
