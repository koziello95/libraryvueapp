using AutoMapper;
using libraryapp.Models;
using libraryVueApp.Model;

namespace libraryVueApp.Dtos
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<CreateUserDto, User>();
        }
    }
}
