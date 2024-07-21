using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        // Create a new article
        public async Task<Article> CreateAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;
        }

        public async Task<Article> GetBySlugAsync(string slug)
        {
            return await _context.Articles
                .Include(a => a.Author)
                .FirstOrDefaultAsync(a => a.Slug == slug);
        }


        // Geet recent articles from users you follow
        public async Task<List<Article>> GetFeedAsync(int pageNumber, int pageSize)
        {
            // Calculate the number of articles to skip
            int skipAmount = (pageNumber - 1) * pageSize;

            return await _context.Articles
                .Include(a => a.Author)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skipAmount) 
                .Take(pageSize) 
                .ToListAsync();
        }


        // Get recent articles globally
        public async Task<List<Article>> GetGlobalAsync(int pageNumber, int pageSize)
        {
            // Calculate the number of articles to skip
            int skipAmount = (pageNumber - 1) * pageSize;

            return await _context.Articles
                  .Include(a => a.Author)
                  .OrderByDescending(a => a.CreatedAt)
                  .Skip(skipAmount)
                  .Take(pageSize) 
                  .ToListAsync();
        }

        // Update an article by slug

        public async Task<Article> UpdateBySlugAsync(string slug, Article updatedArticle)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
            if (article != null)
            {
                // Update properties here
                article.Title = updatedArticle.Title;
                article.Description = updatedArticle.Description;
                article.Body = updatedArticle.Body;
                // Add more properties as needed

                await _context.SaveChangesAsync();
                return article;
            }
            return null;
        }




        /// <summary>
        ///  Delete an article by slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        public async Task<Article> DeleteBySlugAsync(string slug)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return article;
            }
            return null;
        }

    }
}
