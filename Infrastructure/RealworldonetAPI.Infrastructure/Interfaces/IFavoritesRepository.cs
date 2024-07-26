using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Interfaces
{

    public interface IFavoritesRepository
    {
        Task<ArticleFavorite> FavoriteArticleAsync(string slug, string userId);
        Task<bool> UnfavoriteArticleAsync(string slug, string userId);
    }
}
