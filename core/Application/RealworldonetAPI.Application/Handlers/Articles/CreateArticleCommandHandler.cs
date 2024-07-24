using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.Helper;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Articles
{
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateArticleCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        public CreateArticleCommandHandler(
            IArticleRepository articleRepository,
            IMapper mapper,
            ILogger<CreateArticleCommandHandler> logger,
            ICurrentUserService currentUserService)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<ArticleResponseDto> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var article = _mapper.Map<Domain.Entities.Article>(request.CreateArticle);
                article.AuthorId = _currentUserService.GetUserId();
                article.Slug = article.Title.GenerateSlug();

                _logger.LogInformation($"Creating article: {article.Title}");
                var createdArticle = await _articleRepository.CreateAsync(article);
                _logger.LogInformation($"Article created: {createdArticle.Title}");

                return _mapper.Map<ArticleResponseDto>(createdArticle);
            }
            catch (AutoMapperConfigurationException ex)
            {
                _logger.LogError($"Mapping configuration error: {ex.Message}", ex);
                throw new InvalidOperationException($"Mapping configuration error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating article: {ex.Message}", ex);
                throw new InvalidOperationException($"Error creating article: {ex.Message}", ex);
            }
        }
    }
}
