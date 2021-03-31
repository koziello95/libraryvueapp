namespace libraryVueApp.Dtos.UserDtos
{
    public class UserViewModel
    {
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Roles { get; set; }
        public int Id { get; internal set; }
    }
}
