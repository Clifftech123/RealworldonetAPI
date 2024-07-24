using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Queries.Article
{
    public class GetArticlesFeedQuery : IRequest<ArticlesResponseWrapper>
    {
        public int Offset { get; } = 1;
        public int Limit { get; } = 20;

        public GetArticlesFeedQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
    }



}
