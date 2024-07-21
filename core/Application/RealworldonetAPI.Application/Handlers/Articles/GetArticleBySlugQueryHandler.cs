using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleBySlugQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            mapper = _mapper;
        }

        public async Task<ArticleResponseDto> Handle(GetArticleBySlugQuery request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetBySlugAsync(request.Slug);
          var articleResponseDto = _mapper.Map<ArticleResponseDto>(article);
            return articleResponseDto;
        }
    }
}
