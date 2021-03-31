using libraryapp.Models;
using System.Collections.Generic;

namespace libraryVueApp.Data
{
    public interface IBookRepository
    {
        Book GetBook(int id);
        IEnumerable<Book> GetBooks();
        void AddBook(Book book);
        void Update(Book book);
        void Delete(Book book);
    }
}
