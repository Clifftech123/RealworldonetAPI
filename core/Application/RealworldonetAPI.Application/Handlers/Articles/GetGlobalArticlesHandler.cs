using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    /// <summary>
    /// Handles the retrieval of global articles based on various filters.
    /// </summary>
    public class GetGlobalArticlesHandler : IRequestHandler<GetGlobalArticlesQuery, ArticlesResponseWrapper>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetGlobalArticlesHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The repository for managing articles.</param>
        /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
        public GetGlobalArticlesHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the retrieval of global articles based on the specified query parameters.
        /// </summary>
        /// <param name="request">The query containing the filters for retrieving global articles.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A wrapper containing the list of articles and the total count.</returns>
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
