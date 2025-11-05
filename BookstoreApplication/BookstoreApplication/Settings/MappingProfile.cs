using AutoMapper;
using BookstoreApplication.DTOs;
using BookstoreApplication.Models;

namespace BookstoreApplication.Settings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, User>();


            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.ExistsFor, opt => opt.MapFrom(src => DateTime.Now.Year - src.PublishedDate.Year));
            CreateMap<Book, BookDetailsDto>().ReverseMap();
            CreateMap<Book, BookSimpleDto>().ReverseMap();


            CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}
