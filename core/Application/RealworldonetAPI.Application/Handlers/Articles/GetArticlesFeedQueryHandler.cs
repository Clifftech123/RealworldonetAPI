using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class GetArticlesFeedQueryHandler : IRequestHandler<GetArticlesFeedQuery, ArticleResponseDto>

    {

        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesFeedQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ArticleResponseDto> Handle(GetArticlesFeedQuery request, CancellationToken cancellationToken)
        {
           var articles = await _articleRepository.GetFeedAsync(request.PageNumber, request.PageSize);
            var articleResponseDto = _mapper.Map<ArticleResponseDto>(articles);
            return articleResponseDto;
        }
    }
}
