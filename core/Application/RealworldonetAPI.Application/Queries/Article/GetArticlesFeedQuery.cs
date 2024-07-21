using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Queries.Article
{
     public class GetArticlesFeedQuery : IRequest<ArticleResponseDto>
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetArticlesFeedQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
 