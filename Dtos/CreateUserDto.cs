using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
