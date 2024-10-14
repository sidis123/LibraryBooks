using LibraryBE.Models;

namespace LibraryBE.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User Login(string email, string password);
        ICollection<User> GetAllUsers();
        bool UserExists(int id);
        bool Save();
    }
}
