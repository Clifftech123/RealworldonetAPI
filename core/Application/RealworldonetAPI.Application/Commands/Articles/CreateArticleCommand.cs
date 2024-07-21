using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Article
{
     public class CreateArticleCommand : IRequest<ArticleResponseDto>
    {
        public NewArticleDto? CreateArticle { get; }


        public CreateArticleCommand(NewArticleDto createArticle)
        {
            CreateArticle = createArticle;
        }
    }
}
