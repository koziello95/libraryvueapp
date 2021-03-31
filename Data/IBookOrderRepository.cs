using libraryapp.Models;
using libraryVueApp.Model;
using System.Collections.Generic;

namespace libraryVueApp.Data
{
    public interface IBookOrderRepository
    {
        RequestBookResult RequestBook(Book book, User user);
        ReturnBookResult ReturnBook(BookOrder bookOrder);
        DisposeBookResult DisposeBook(BookOrder bookOrder);
        BookOrder GetLatestBookOrder(int bookId, int returningUserId);
        IEnumerable<BookOrder> GetBookOrders();
        List<BookOrder> GetBookOrders(int bookId, bool includeUsers = false);
        BookOrder GetById(int bookOrderId);
        UserLimitCheckResult HasReachedLimitOfBooksBorrowed(int userId);
        HasOverDueBooksResult UserHasOverdueBookOrders(int userId);
    }

    public class DisposeBookResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }

    public class ReturnBookResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }

    public class RequestBookResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }
    public class UserLimitCheckResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }

    public class HasOverDueBooksResult
    {
        public bool Success { get; internal set; }
        public string Message { get; internal set; }
    }

}
