using Library.API.Data.Entities;

namespace Library.API.Interface
{
    public interface IBookRepository
    {
        bool Add(Book book);
        bool Update(Book book);
        bool Delete(Book book);
        Book GetBookById(string id);
        IEnumerable<Book> GetAll();
    }
}

