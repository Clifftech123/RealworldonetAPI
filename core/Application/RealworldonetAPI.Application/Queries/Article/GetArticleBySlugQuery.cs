using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Queries.Article
{
    public class GetArticleBySlugQuery : IRequest<List<ArticleResponseDto>>
    {
        public string Slug { get; set; }

        public GetArticleBySlugQuery(string slug)
        {
            Slug = slug;
        }



    }
}
