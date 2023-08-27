using Library.API.Data.Context;
using Library.API.Data.Entities;
using Library.API.Interface;

namespace Library.API.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryContext _context;
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }

        public bool Add(Book book)
        {
            _context.Add(book);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Book book)
        {
            _context.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
            return true;
        }

        public Book GetBookById (string id)
        {
            var result = _context.Books.FirstOrDefault(x => x.Id == id);
            if (result != null)
                return result;

            return new Book();
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books.ToList(); ;
        }
    }
}
