using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Interfaces
{
    public interface IArticleRepository
    {
        // Create an article
        Task<Article> CreateAsync(Article article);

        // Get recent articles from users you follow
        Task<List<Article>> GetFeedAsync(int offset, int limit);

        // Updated GetGlobalAsync method signature with optional parameters and default values
        Task<List<Article>> GetGlobalAsync(string? tag = null, string? author = null, string? favorited = null, int offset = 1, int limit = 20);

        // Get an article by slug
        Task<Article> GetBySlugAsync(string slug);

        // Update an article by slug
        Task<Article> UpdateBySlugAsync(string slug, Article article);

        // Delete an article by slug
        Task<Article> DeleteBySlugAsync(string slug);


        Task<int> GetTotalCountAsync();
    }
}
