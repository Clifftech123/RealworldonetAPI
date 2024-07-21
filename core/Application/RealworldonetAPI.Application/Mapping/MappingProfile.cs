using AutoMapper;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Mapping
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapping from NewArticleDto to Article
            CreateMap<NewArticleDto, Article>().ReverseMap();

            // Mapping from UpdateArticleDto to Article
            CreateMap<UpdateArticleDto, Article>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Mapping from Article to ArticleResponseDto, including the nested author profile
            CreateMap<Article, ArticleResponseDto>().ReverseMap()
                .ForMember(dest => dest.Author, act => act.MapFrom(src => src.Author));

            // Mapping from User (Author) to ProfileDto
            CreateMap<ApplicationUser, UserProfile>().ReverseMap();
        }
    }
}
