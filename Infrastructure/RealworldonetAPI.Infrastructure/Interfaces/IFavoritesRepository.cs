using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Interfaces
{

    public interface IFavoritesRepository
    {
        Task<ArticleFavorite> FavoriteArticleAsync(string slug);
        Task<bool> UnfavoriteArticleAsync(string slug);
    }
}
