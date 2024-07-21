using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Article
{
    public class UpdateArticleCommand : IRequest<ArticleResponseDto>
    {
        public string Slug { get; }

        public UpdateArticleDto? UpdateArticle { get; }

        public UpdateArticleCommand(string slug, UpdateArticleDto updateArticle)
        {
            Slug = slug;
            UpdateArticle = updateArticle;
        }
    }
}
