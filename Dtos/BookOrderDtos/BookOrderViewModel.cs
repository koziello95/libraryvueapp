using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using libraryVueApp.Model;

namespace libraryVueApp.Dtos
{
    public class BookOrderViewModel
    {
        public int BookOrderId { get; internal set; }
        public string Lastname { get; internal set; }
        public string Firstname { get; internal set; }
        public string Login { get; internal set; }
        public OrderStatus Status { get; internal set; }
        public int UserId { get; internal set; }
        public string ExpectedReturnDate { get; internal set; }
    }
}
