
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
    /// <summary>  
    /// Handles the creation of an article.  
    /// </summary>  
    public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponseDto>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateArticleCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>  
        /// Initializes a new instance of the <see cref="CreateArticleCommandHandler"/> class.  
        /// </summary>  
        /// <param name="articleRepository">The repository for managing articles.</param>  
        /// <param name="mapper">The mapper for converting between entities and DTOs.</param>  
        /// <param name="logger">The logger for logging information and errors.</param>  
        /// <param name="currentUserService">The service for retrieving the current user.</param>  
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

        /// <summary>  
        /// Handles the creation of an article.  
        /// </summary>  
        /// <param name="request">The command containing the details of the article to create.</param>  
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
        /// <returns>The details of the created article.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when an error occurs during article creation.</exception>  
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
