using AutoMapper;
using Bookstore.Application.DTOs;
using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.AwardEntities;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Entities.PublisherEntities;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Bookstore.Domain.ExternalEntities.ComicEntities;

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
            CreateMap<Publisher, PublisherDto>().ReverseMap();

            CreateMap<Award, AwardDto>().ReverseMap();


            CreateMap<ComicIssueExt, ComicIssue>()
                .ForMember(dest => dest.ExternalIssueId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CoverDate, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.CoverDate, DateTimeKind.Utc)));

            CreateMap<ComicIssueCreateDto, ComicIssue>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<ReviewCreateRequestDto, Review>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
