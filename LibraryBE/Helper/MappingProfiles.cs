using AutoMapper;
using LibraryBE.DTO;
using LibraryBE.Models;

namespace LibraryBE.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationDto, Reservation>();
        }
    }
}
