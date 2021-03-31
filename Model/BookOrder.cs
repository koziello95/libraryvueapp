using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libraryVueApp.Model
{
    public class BookOrder
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime ExpectedReturnDate { get; set; }

        public User User { get; set; }

    }
    public enum OrderStatus
    {
        Requested = 1,        
        Borrowed = 2,
        Returned = 3
    }

}
