using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryapp.Models;

namespace libraryVueApp.Data
{
    public class MockBookRepository : IBookRepository
    {
        public void AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void Delete(Book book)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            return new Book
            {
                Author = "Stephen King",
                Title = "The Shining",
                YearPublished = 1997,
                Description = "Jack and his family move into an isolated hotel with a violent past. Living in isolation, Jack begins to lose his sanity, which affects his family members.",
                Id = 1
            };
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = new Book[]
            {
                 new Book
            {
                Author = "Stephen King",
                Title = "The Shining",
                YearPublished = 1997,
                Description = "Jack and his family move into an isolated hotel with a violent past. Living in isolation, Jack begins to lose his sanity, which affects his family members.",
                Id = 1
            },
                  new Book
            {
                Author = "Fiodor Dostoyevsky",
                Title = "Karamazov Brothers",
                YearPublished = 1910,
                Description = "Three brothers having issues",
                Id = 2
            },
                   new Book
            {
                Author = "Borys Cośtam",
                Title = "Doktor Żywago",
                YearPublished = 1997,
                Description = "Long life of a very interesting doctor",
                Id = 3
            }
            };

            return books;
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Book book)
        {
            throw new NotImplementedException();
        }
    }
}
