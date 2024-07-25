
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    /// <summary>  
    /// Repository for managing articles in the database.  
    /// </summary>  
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>  
        /// Initializes a new instance of the <see cref="ArticleRepository"/> class.  
        /// </summary>  
        /// <param name="context">The database context.</param>  
        /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>  
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>  
        /// Creates a new article asynchronously.  
        /// </summary>  
        /// <param name="article">The article to create.</param>  
        /// <returns>The created article.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error creating the article.</exception>  
        public async Task<Article> CreateAsync(Article article)
        {
            try
            {

                await _context.Articles.AddAsync(article);
                await _context.SaveChangesAsync();
                return article;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating article", ex);
            }
        }

        /// <summary>  
        /// Retrieves an article by its slug asynchronously.  
        /// </summary>  
        /// <param name="slug">The slug of the article.</param>  
        /// <returns>The article with the specified slug.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error retrieving the article.</exception>  
        public async Task<Article> GetBySlugAsync(string slug)
        {
            try
            {
                return await _context.Articles
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync(a => a.Slug == slug);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving article with slug {slug}", ex);
            }
        }

        /// <summary>  
        /// Retrieves a feed of articles with pagination asynchronously.  
        /// </summary>  
        /// <param name="offset">The offset for pagination.</param>  
        /// <param name="limit">The limit for pagination.</param>  
        /// <returns>A list of articles.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error retrieving the article feed.</exception>  
        public async Task<List<Article>> GetFeedAsync(int offset, int limit)
        {
            try
            {
                // Ensure the offset is within the range 1-20  
                if (offset < 1 || offset > 20)
                {
                    throw new ArgumentOutOfRangeException(nameof(offset), "Offset must be between 1 and 20.");
                }

                // Calculate the amount to skip based on the offset and limit  
                int skipAmount = (offset - 1) * limit;

                // Retrieve the articles with pagination  
                return await _context.Articles
                    .Include(a => a.Author)
                    .OrderByDescending(a => a.CreatedAt)
                    .Skip(skipAmount)
                    .Take(limit)
                    .ToListAsync();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new InvalidOperationException("Invalid offset value provided.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving article feed", ex);
            }
        }

        /// <summary>  
        /// Retrieves a global list of articles with optional filters asynchronously.  
        /// </summary>  
        /// <param name="tag">The tag to filter by.</param>  
        /// <param name="author">The author to filter by.</param>  
        /// <param name="favorited">The user who favorited the articles to filter by.</param>  
        /// <param name="offset">The offset for pagination.</param>  
        /// <param name="limit">The limit for pagination.</param>  
        /// <returns>A list of articles.</returns>  
        public async Task<List<Article>> GetGlobalAsync(string? tag = null, string? author = null, string? favorited = null, int offset = 1, int limit = 20)
        {
            var query = _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Tags)
                .Include(a => a.ArticleFavorites)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.Where(a => a.Tags.Any(t => t.Name.ToLower() == tag.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(a => a.Author.UserName.ToLower() == author.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(favorited))
            {
                query = query.Where(a => a.ArticleFavorites.Any(f => f.User.UserName.ToLower() == favorited.ToLower()));
            }

            int skipAmount = (offset - 1) * limit;

            return await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipAmount)
                .Take(limit)
                .ToListAsync();
        }

        /// <summary>  
        /// Updates an article by its slug asynchronously.  
        /// </summary>  
        /// <param name="slug">The slug of the article to update.</param>  
        /// <param name="updatedArticle">The updated article details.</param>  
        /// <returns>The updated article.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error updating the article.</exception>  
        public async Task<Article> UpdateBySlugAsync(string slug, Article updatedArticle)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Article with slug {slug} not found.");
                }



                article.Title = updatedArticle.Title;
                article.Description = updatedArticle.Description;
                article.Body = updatedArticle.Body;
                await _context.SaveChangesAsync();

                return article;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error updating article with slug {slug}", ex);
            }
        }

        /// <summary>  
        /// Deletes an article by its slug asynchronously.  
        /// </summary>  
        /// <param name="slug">The slug of the article to delete.</param>  
        /// <returns>The deleted article.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error deleting the article.</exception>  
        public async Task<Article> DeleteBySlugAsync(string slug)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
                if (article == null)
                {
                    throw new KeyNotFoundException($"Article with slug {slug} not found.");
                }

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return article;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting article with slug {slug}", ex);
            }
        }

        /// <summary>  
        /// Retrieves the total count of articles asynchronously.  
        /// </summary>  
        /// <returns>The total count of articles.</returns>  
        /// <exception cref="InvalidOperationException">Thrown when there is an error retrieving the total article count.</exception>  
        public async Task<int> GetTotalCountAsync()
        {
            try
            {
                return await _context.Articles.CountAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving total article count", ex);
            }
        }
    }
}
