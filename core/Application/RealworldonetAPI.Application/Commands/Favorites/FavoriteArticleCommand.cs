using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Favorites
{
     public class FavoriteArticleCommand : IRequest<ArticleResponseDto>
    {
        public string Slug { get; }
        
        public FavoriteArticleCommand(string slug)
        {
            Slug = slug;
           
        }
    }
}
