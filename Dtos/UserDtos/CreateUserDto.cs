using System.ComponentModel.DataAnnotations;

namespace libraryVueApp.Dtos
{
    public class CreateUserDto
    {        
        [Required]
        public string Login { get; set; }
        [MinLength(6)]
        public string Password { get; set; }       
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
