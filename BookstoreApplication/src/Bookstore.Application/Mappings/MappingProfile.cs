using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.UserEntities;

namespace Bookstore.Application.Mappings
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
            CreateMap<Author, AuthorNameDto>();
        }
    }
}
