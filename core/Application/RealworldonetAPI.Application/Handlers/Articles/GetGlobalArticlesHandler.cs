using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class GetGlobalArticlesHandler : IRequestHandler<GetGlobalArticlesQuery, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;
         readonly IMapper _mapper;

        public GetGlobalArticlesHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ArticleResponseDto> Handle(GetGlobalArticlesQuery request, CancellationToken cancellationToken)
        {

          var articles = await _articleRepository.GetGlobalAsync(request.PageNumber, request.PageSize);
            var articleResponseDto = _mapper.Map<ArticleResponseDto>(articles);
            return articleResponseDto;
        }
    }
}
