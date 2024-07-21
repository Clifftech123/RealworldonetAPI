using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;

         private readonly IMapper _mapper;

        public UpdateArticleHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public async Task<ArticleResponseDto> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var existingArticle = await _articleRepository.GetBySlugAsync(request.Slug);

            if (existingArticle == null)
            {
                throw new Exception("Article not found");
            }
            var article = _mapper.Map<Domain.Entities.Article>(request.UpdateArticle);
            var updatedArticle = await _articleRepository.UpdateBySlugAsync(request.Slug, article);
            var articleResponseDto = _mapper.Map<ArticleResponseDto>(updatedArticle);

            return articleResponseDto;
        }

    }
}
