using libraryapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
