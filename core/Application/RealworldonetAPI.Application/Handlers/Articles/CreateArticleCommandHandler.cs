using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Articles
{

    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public async Task<ArticleResponseDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {

            var article = _mapper.Map<Domain.Entities.Article>(request.CreateArticle);
            // Save the article to the database
            var createdArticle = await _articleRepository.CreateAsync(article);
            var articleResponseDto = _mapper.Map<ArticleResponseDto>(createdArticle);

            // Return the response
            return articleResponseDto;
        }

    }
}
