using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace libraryVueApp.Model
{
    public class Role
    {
        [Key]
        public RoleType Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public enum RoleType
    {
        Librarian = 1,
        Reader = 2
    }
}
