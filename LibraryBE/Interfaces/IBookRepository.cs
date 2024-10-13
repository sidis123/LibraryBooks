using LibraryBE.Models;
namespace LibraryBE.Interfaces
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        ICollection<Book> GetAllBooks();
        bool BookExists(int id);
        bool Save();
    }
}
