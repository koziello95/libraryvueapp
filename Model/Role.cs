using System.ComponentModel.DataAnnotations;

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
