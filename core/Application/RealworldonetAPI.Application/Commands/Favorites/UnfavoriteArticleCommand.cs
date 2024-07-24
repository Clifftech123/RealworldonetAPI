
using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Favorites
{
    public class UnfavoriteArticleCommand : IRequest<ArticleResponseDto>
    {
        public string Slug { get; }

        public UnfavoriteArticleCommand(string slug)
        {
            Slug = slug;

        }
    }
}
