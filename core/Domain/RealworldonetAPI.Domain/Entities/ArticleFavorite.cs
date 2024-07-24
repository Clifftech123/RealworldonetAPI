namespace RealworldonetAPI.Domain.Entities
{


    public class ArticleFavorite(string username, Guid articleId)
    {
        public string Username { get; set; } = username;

        public Guid ArticleId { get; set; } = articleId;

        public ApplicationUser User { get; set; } = null!;

        public Article Article { get; set; } = null!;
    }
}
