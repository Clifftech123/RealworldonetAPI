using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Interfaces
{
    public interface IArticleRepository
    {
        // Create an article

        Task<Article> CreateAsync(Article article);

        // Get recent articles from users you follow
        Task<List<Article>> GetFeedAsync(int pageNumber, int pageSize);

        // Get recent articles globally
        Task<List<Article>> GetGlobalAsync(int pageNumber, int pageSize);

        // Get an article by slug
        Task<Article> GetBySlugAsync(string slug);

        // Update an article by slug
        Task<Article> UpdateBySlugAsync(string slug, Article article);

        // Delete an article by slug
        Task<Article> DeleteBySlugAsync(string slug);
    }
}
