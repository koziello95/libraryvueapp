using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryapp.Models;

namespace libraryVueApp.Data
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly LibraryContext _context;

        public SqlBookRepository(LibraryContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Add(book);
        }

        public void Delete(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Remove(book);
        }

        public Book GetBook(int id)
        {
            return _context.Books.Find(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() == 0;
        }

        public void Update(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            _context.Books.Update(book);
        }
    }
}
