
using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Favorites
{
    /// <summary>
    /// Handles the command to favorite an article.
    /// </summary>
    public class FavoriteArticleCommandHandler : IRequestHandler<FavoriteArticleCommand, ArticleResponseDto>
    {
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteArticleCommandHandler"/> class.
        /// </summary>
        /// <param name="favoriteRepository">The repository for managing article favorites.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="currentUserService">The service to get the current user information.</param>
        /// <param name="context">The application database context.</param>
        public FavoriteArticleCommandHandler(IFavoritesRepository favoriteRepository, IMapper mapper, ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _context = context;
        }

        /// <summary>
        /// Handles the command to favorite an article.
        /// </summary>
        /// <param name="request">The command request containing the article slug.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The response DTO containing the favorited article information.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the article is not found, a database error occurs, or a mapping error occurs.
        /// </exception>
        public async Task<ArticleResponseDto> Handle(FavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                var article = await _context.Articles
                    .Where(a => a.Slug == request.Slug)
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync();

                if (article == null)
                {
                    throw new InvalidOperationException("Article not found or you do not have permission to view it.");
                }

                var favoritedArticle = await _favoriteRepository.FavoriteArticleAsync(request.Slug, userId);
                var articleResponseDto = _mapper.Map<ArticleResponseDto>(favoritedArticle);
                return articleResponseDto;
            }
            catch (Exception ex) when (ex is SqlException || ex is System.Data.Entity.Core.EntityException)
            {
                // Handle database-related exceptions
                throw new InvalidOperationException("A database error occurred while favoriting the article.", ex);
            }
            catch (AutoMapperMappingException)
            {
                // Handle mapping exceptions
                throw new InvalidOperationException("An error occurred while mapping the article for the favorite operation.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}
