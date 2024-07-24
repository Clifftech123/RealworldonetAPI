using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class GetGlobalArticlesHandler : IRequestHandler<GetGlobalArticlesQuery, ArticlesResponseWrapper>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;


        public GetGlobalArticlesHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ArticlesResponseWrapper> Handle(GetGlobalArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _articleRepository.GetGlobalAsync(
                tag: request.Tag,
                author: request.Author,
                favorited: request.Favorited,
                offset: request.PageNumber,
                limit: request.PageSize);

            var articleResponseDto = new ArticlesResponseWrapper
            {
                Articles = _mapper.Map<List<ArticleResponseDto>>(articles),
                ArticlesCount = articles.Count
            };

            return articleResponseDto;
        }
    }

}
