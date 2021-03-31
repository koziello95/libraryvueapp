﻿using libraryapp.Models;
using libraryVueApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libraryVueApp.Data
{
    public interface IBookOrderRepository
    {
        RequestBookResult RequestBook(Book book, User user);
        ReturnBookResult ReturnBook(BookOrder bookOrder);
        GiveBookResult GiveBook(BookOrder bookOrder);
        BookOrder GetLatestBookOrder(int bookId, int returningUserId);
        IEnumerable<BookOrder> GetBookOrders();
        List<BookOrder> GetBookOrders(int bookId, bool includeUsers = false);
        bool SaveChanges();
    }

    public class GiveBookResult
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
}
