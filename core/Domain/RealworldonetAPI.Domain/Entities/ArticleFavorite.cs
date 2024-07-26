namespace RealworldonetAPI.Domain.Entities
{
    public record ArticleFavorite
    {
        public Guid ArticleId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Article Article { get; set; }

        public ArticleFavorite(string userId, Guid articleId)
        {
            UserId = userId;
            ArticleId = articleId;
        }
    }
}
