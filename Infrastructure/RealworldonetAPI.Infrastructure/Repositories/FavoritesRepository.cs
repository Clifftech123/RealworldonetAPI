using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing favorite articles.
    /// </summary>
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FavoritesRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="logger">The logger instance.</param>
        public FavoritesRepository(ApplicationDbContext context, ILogger<FavoritesRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Favorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to favorite.</param>
        /// <param name="userId">The ID of the user favoriting the article.</param>
        /// <returns>The favorited article.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the article is not found.</exception>
        public async Task<ArticleFavorite> FavoriteArticleAsync(string slug, string userId)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);

            if (article == null)
            {
                throw new InvalidOperationException("Article not found.");
            }

            // If the user has already favorited the article, return the existing favorite
            var existingFavorite = await _context.ArticleFavorites
                .FirstOrDefaultAsync(f => f.ArticleId == article.Id && f.UserId == userId);

            if (existingFavorite != null)
            {
                return existingFavorite;
            }

            var favorite = new ArticleFavorite(userId, article.Id);

            _context.ArticleFavorites.Add(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        /// <summary>
        /// Unfavorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to unfavorite.</param>
        /// <param name="userId">The ID of the user unfavoriting the article.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public async Task<bool> UnfavoriteArticleAsync(string slug, string userId)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
                if (article == null)
                {
                    _logger.LogWarning("Article with slug {Slug} not found.", slug);
                    return false;
                }

                var favorite = await _context.ArticleFavorites
                    .FirstOrDefaultAsync(f => f.ArticleId == article.Id && f.UserId == userId);

                if (favorite == null)
                {
                    _logger.LogWarning("Favorite for article {ArticleId} and user {UserId} not found.", article.Id, userId);
                    return false;
                }

                _context.ArticleFavorites.Remove(favorite);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while unfavoriting the article with slug {Slug} for user {UserId}.", slug, userId);
                throw new InvalidOperationException("An error occurred while mapping the article for the unfavorite operation.", ex);
            }
        }
    }
}
