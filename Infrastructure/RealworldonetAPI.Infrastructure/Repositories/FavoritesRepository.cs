using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;
using System.Security.Claims;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing favorite articles.
    /// </summary>
    public class FavoritesRepository : IFavoritesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesRepository"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public FavoritesRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private string GetCurrentUsername()
        {
            var username = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(username))
            {
                throw new InvalidOperationException("User not found.");
            }
            return username;
        }

        /// <summary>
        /// Favorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to favorite.</param>
        /// <returns>The favorited article.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the article or user is not found.</exception>
        public async Task<ArticleFavorite> FavoriteArticleAsync(string slug)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
            if (article == null)
            {
                throw new InvalidOperationException("Article not found.");
            }

            var username = GetCurrentUsername();

            var favorite = new ArticleFavorite(username, article.Id);

            await _context.ArticleFavorites.AddAsync(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        /// <summary>
        /// Unfavorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to unfavorite.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the user is not found.</exception>
        public async Task<bool> UnfavoriteArticleAsync(string slug)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
            if (article == null)
            {
                return false;
            }

            var username = GetCurrentUsername();

            var favorite = await _context.ArticleFavorites
                .FirstOrDefaultAsync(f => f.ArticleId == article.Id && f.Username == username);

            if (favorite == null)
            {
                return false;
            }

            _context.ArticleFavorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
