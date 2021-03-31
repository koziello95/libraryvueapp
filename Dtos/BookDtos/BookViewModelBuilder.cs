using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryapp.Models;
using libraryVueApp.Model;

namespace libraryVueApp.Dtos.BookDtos
{
    public class BookViewModelBuilder
    {
        private List<Book> _books;
        private List<Model.BookOrder> _bookOrders;
        private readonly int _userId;

        public BookViewModelBuilder(List<Book> books, List<BookOrder> bookOrders, int userId)
        {
            _books = books;
            _bookOrders = bookOrders;
            _userId = userId;
        }

        internal IEnumerable<BookViewModel> Build()
        {
            return _books.Select(b => new BookViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                YearPublished = b.YearPublished,
                Description = b.Description,
                Status = ResolveStatus(b, _bookOrders, _userId),
                QueueLength = _bookOrders.Count(bo => bo.BookId == b.Id && bo.OrderStatus == OrderStatus.Requested)
            }); ;

        }

        private BookStatus ResolveStatus(Book book, IEnumerable<BookOrder> bookOrders, int userId)
        {
            if (_bookOrders.Any(bo => bo.BookId == book.Id && bo.UserId == _userId && bo.OrderStatus == OrderStatus.Requested))
                return BookStatus.AlreadyRequested;

            return _bookOrders.Any(bo => bo.BookId == book.Id && bo.OrderStatus == OrderStatus.Borrowed) ? BookStatus.Taken : BookStatus.Free;                
        }
    }
   
}
