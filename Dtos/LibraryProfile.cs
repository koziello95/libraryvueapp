using AutoMapper;
using libraryapp.Models;
using libraryVueApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
