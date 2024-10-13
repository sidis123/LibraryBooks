using LibraryBE.Models;

namespace LibraryBE.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        ICollection<User> GetAllUsers();
        bool UserExists(int id);
        bool Save();
    }
}
