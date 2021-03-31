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
        private int _maxNumberOfBorrowedBooks;

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

        public DisposeBookResult DisposeBook(BookOrder bookOrder)
        {
            Contracts.Require(bookOrder != null, "BookOrder argument cannot be null");

            if (_libraryContext.BookOrders.Any(o => o.BookId == bookOrder.BookId && o.OrderStatus == OrderStatus.Borrowed))
                return new DisposeBookResult
                {
                    Success = false,
                    Message = "Book was already taken! You can't give it to user"
                };

            bookOrder.OrderStatus = OrderStatus.Borrowed;
            _libraryContext.BookOrders.Update(bookOrder);
            return new DisposeBookResult
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

            if (pendingBookRequests.Any(bo => bo.OrderStatus == OrderStatus.Borrowed && bo.UserId == user.Id))
                return new RequestBookResult
                {
                    Success = false,
                    Message = $"You already have that book borrowed. You can't get into queue."
                };

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
            _libraryContext.SaveChanges();
            return true;
        }

        public BookOrder GetById(int bookOrderId)
        {
            return _libraryContext.BookOrders.Find(bookOrderId);
        }

        public UserLimitCheckResult HasReachedLimitOfBooksBorrowed(int userId)
        {
            var numberOfBooksBorrowed = _libraryContext.BookOrders.Count(bo => bo.UserId == userId && bo.OrderStatus == OrderStatus.Borrowed);
            if (numberOfBooksBorrowed >= _maxNumberOfBorrowedBooks)
                return new UserLimitCheckResult
                {
                    Message = $"User has reached a limit of {_maxNumberOfBorrowedBooks} borrowed books at the same time. Please return any book to borrow next one.",
                    Success = false
                };
            return new UserLimitCheckResult
            {
                Success = true
            };
        }

        public HasOverDueBooksResult UserHasOverdueBookOrders(int userId)
        {
            var hasOverdueBooks = _libraryContext.BookOrders
                .Any(bo => bo.UserId == userId && bo.OrderStatus == OrderStatus.Borrowed && bo.ExpectedReturnDate > DateTime.UtcNow);


            if (hasOverdueBooks)
                return new HasOverDueBooksResult
                {
                    Message = "You have overdue books. Please return them as soon as possible. Further borrowings are blocked",
                    Success = false
                };

            return new HasOverDueBooksResult
            {
                Success = true
            };
        }
    }
}
