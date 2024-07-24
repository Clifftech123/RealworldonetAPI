using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Queries.Article
{
    public class GetGlobalArticlesQuery : IRequest<ArticlesResponseWrapper>
    {
        public string? Tag { get; }
        public string? Author { get; }
        public string? Favorited { get; }
        public int PageNumber { get; }
        public int PageSize { get; }

        public GetGlobalArticlesQuery(string? tag = null, string? author = null, string? favorited = null, int pageNumber = 1, int pageSize = 20)
        {
            Tag = tag;
            Author = author;
            Favorited = favorited;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
