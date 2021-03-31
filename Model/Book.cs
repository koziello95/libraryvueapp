using libraryVueApp.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace libraryapp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int YearPublished { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        public List<BookOrder> BookOrders { get; set; }
    }
}
