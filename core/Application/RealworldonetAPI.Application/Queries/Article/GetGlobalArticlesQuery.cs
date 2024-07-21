using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Queries.Article
{
      public class GetGlobalArticlesQuery : IRequest<ArticleResponseDto>
    {
        public int PageNumber { get; } 
        public int PageSize { get; }

        public GetGlobalArticlesQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
