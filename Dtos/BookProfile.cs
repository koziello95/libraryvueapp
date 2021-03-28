using AutoMapper;
using libraryapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libraryVueApp.Dtos
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookDto, Book>().ForMember(bookdto => bookdto.Description, book => book.MapFrom(dto => dto.Description));
            
        }
    }
}
