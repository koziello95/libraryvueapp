using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryapp.Models;
using libraryVueApp.Model;
using libraryVueApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace libraryVueApp.Data
{
    public class BookOrderRepository : IBookOrderRepository
    {
        private readonly LibraryContext _libraryContext;

        public BookOrderRepository(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
        }

        public IEnumerable<BookOrder> GetBookOrders()        {

            return _libraryContext.BookOrders.ToList();
        }

      

        public List<BookOrder> GetBookOrders(int bookId, bool includeUsers = false)
        {
            if(includeUsers)
                return _libraryContext.BookOrders
                    .Include(bo => bo.User)
                    .Where(bo => bo.BookId == bookId)
                    .ToList();

            return _libraryContext.BookOrders
                .Where(bo => bo.BookId == bookId)
                .ToList();
        }

        public BookOrder GetLatestBookOrder(int bookId, int userId)
        {
            return _libraryContext.BookOrders
                .Where(bo => bo.BookId == bookId && bo.UserId == userId)
                .OrderByDescending(bo => bo.ExpectedReturnDate)
                .First();
            //this query pulls all specific book-user orders in case one user borrowed a given more more than once
        }

        public GiveBookResult GiveBook(BookOrder bookOrder)
        {
            Contracts.Require(bookOrder != null, "BookOrder argument cannot be null");

            if (_libraryContext.BookOrders.Any(o => o.BookId == bookOrder.BookId && o.OrderStatus == OrderStatus.Borrowed))
                return new GiveBookResult
                {
                    Success = false,
                    Message = "Book was already taken! You can't give it to user"
                };

            bookOrder.OrderStatus = OrderStatus.Borrowed;
            _libraryContext.BookOrders.Update(bookOrder);
            return new GiveBookResult
            {
                Success = true,
                Message = "Sucessfully saved that book was given to reader"
            };
        }
    

        public RequestBookResult RequestBook(Book book, User user)
        {
            Contracts.Require(book != null, "Book argument cannot be null");
            Contracts.Require(user != null, "User argument cannot be null");

            _libraryContext.BookOrders.Add(new BookOrder
            {
                BookId = book.Id,
                UserId = user.Id,
                OrderStatus = OrderStatus.Requested,
                ExpectedReturnDate = DateTime.UtcNow
            });

            var pendingBookRequests = _libraryContext.BookOrders.Where(bookOrder => bookOrder.BookId == book.Id && bookOrder.OrderStatus != OrderStatus.Returned).ToArray();

            if (pendingBookRequests.Any(bo => bo.OrderStatus == OrderStatus.Borrowed))
                return new RequestBookResult
                {
                    Success = true,
                    Message = $"Order was places successfully! Book is already borrowed, your position in queue is {pendingBookRequests.Count(bo => bo.OrderStatus == OrderStatus.Requested)}"
                };

            return new RequestBookResult
            {
                Success = true,
                Message = "Order was places successfully! Please go to library to get your book."
            };
        }

        public ReturnBookResult ReturnBook(BookOrder bookOrder)
        {
            Contracts.Require(bookOrder != null, "BookOrder argument cannot be null");

            bookOrder.OrderStatus = OrderStatus.Returned;
            _libraryContext.BookOrders.Update(bookOrder);
                return new ReturnBookResult
                {
                    Success = true,
                    Message = "Successfully returned book to the library!"
                };
        }

        public bool SaveChanges()
        {
            return _libraryContext.SaveChanges() == 0;
        }
    }
}
