using System.ComponentModel.DataAnnotations;

namespace libraryVueApp.Dtos
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int YearPublished { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
    }
}
