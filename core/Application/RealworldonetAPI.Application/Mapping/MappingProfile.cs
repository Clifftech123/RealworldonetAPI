﻿using AutoMapper;
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
  .ForMember(dest => dest.Following, opt => opt.Ignore())
  .ForMember(dest => dest.Password, opt => opt.Ignore());


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
                //.ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => src.ArticleFavorites.Count))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            CreateMap<ArticleFavorite, ArticleResponseDto>()
                .IncludeMembers(src => src.Article)
                .ForMember(dest => dest.Favorited, opt => opt.MapFrom((src, dest, _, context) => true));
            //.ForMember(dest => dest.FavoritesCount, opt => opt.MapFrom(src => src.Article.ArticleFavorites.Count));

            CreateMap<Comment, CommentDetailDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));

            // Add the missing mapping
            CreateMap<CreateCommentWrapper, Comment>()
                .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Comment.Body))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Author, opt => opt.Ignore())
                .ForMember(dest => dest.Article, opt => opt.Ignore())
                .ForMember(dest => dest.ArticleId, opt => opt.Ignore())
                .ForMember(dest => dest.AuthorId, opt => opt.Ignore());
        }
    }
}
