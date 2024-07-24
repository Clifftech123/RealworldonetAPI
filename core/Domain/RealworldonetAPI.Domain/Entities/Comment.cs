namespace RealworldonetAPI.Domain.Entities
{

    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public string Username { get; set; }
        public Guid ArticleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

        public ApplicationUser Author { get; set; } = null!;
        public Article Article { get; set; } = null!;
    }

}
