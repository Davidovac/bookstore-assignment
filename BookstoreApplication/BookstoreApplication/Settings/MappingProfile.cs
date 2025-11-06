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
            CreateMap<User, ProfileDto>();


            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.ExistsFor, opt => opt.MapFrom(src => DateTime.UtcNow.Year - src.PublishedDate.Year));
            CreateMap<Book, BookDetailsDto>().ReverseMap();
            CreateMap<Book, BookRequestDto>().ReverseMap()
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.PublishedDate, DateTimeKind.Utc)));


            CreateMap<Author, AuthorDto>().ReverseMap();
        }
    }
}
