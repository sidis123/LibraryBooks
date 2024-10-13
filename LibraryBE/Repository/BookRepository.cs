using LibraryBE.Data;
using LibraryBE.Interfaces;
using LibraryBE.Models;

namespace LibraryBE.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }
        public bool BookExists(int id)
        {
            return _context.Books.Any(b => b.id_Book == id);
        }

        public ICollection<Book> GetAllBooks()
        {
            return _context.Books.OrderBy(b => b.id_Book).ToList();
        }

        public Book GetBook(int id)
        {
            return _context.Books.Where(b => b.id_Book == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
