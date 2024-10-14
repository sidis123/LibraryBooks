using LibraryBE.Data;
using LibraryBE.Interfaces;
using LibraryBE.Models;

namespace LibraryBE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<User> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.id_User).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.id_User == id).FirstOrDefault();
        }

        public User Login(string email, string password)
        {
            var user = _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
            return user;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(u => u.id_User == id);
        }
    }
}
