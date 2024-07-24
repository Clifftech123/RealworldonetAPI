using AutoMapper;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.DTO.comments;
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


            CreateMap<ApplicationUser, UserProfiledto>()
 .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
 .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
 .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
 .ForMember(dest => dest.Following, opt => opt.MapFrom(src => src.following));

            CreateMap<ApplicationUser, AuthorDto>()
.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
.ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image ?? "https://api.realworld.io/images/demo-avatar.png"))
.ForMember(dest => dest.Following, opt => opt.Ignore());

            CreateMap<NewArticleDto, Article>()
                 .ForMember(dest => dest.AuthorId, opt => opt.Ignore())
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagList.Select(tag => new Tag { Name = tag })));
                





            CreateMap<Article, ArticleResponseDto>()
  .ForMember(dest => dest.TagList, opt => opt.MapFrom(src => src.Tags.Select(t => t.Name).ToList()))
  .ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => src.ArticleFavorites.Count))
  .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));





            CreateMap<Comment, CreateCommentDto>()
  .ForPath(dest => dest.Comment.Body, opt => opt.MapFrom(src => src.Body));
            CreateMap<CreateCommentDto, Comment>()
           .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Comment.Body));





        }
    }
}
