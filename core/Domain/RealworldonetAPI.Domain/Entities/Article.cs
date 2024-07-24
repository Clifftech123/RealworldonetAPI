using RealworldonetAPI.Domain.Helper;

namespace RealworldonetAPI.Domain.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; } = 0;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public List<Comment> Comments { get; set; } = new();
        public ICollection<ArticleFavorite> ArticleFavorites { get; set; } = new List<ArticleFavorite>();

        // Parameterless constructor
        public Article() { }

        // Constructor with parameters
        public Article(string title, string description, string body, string authorId)
        {
            Title = title;
            Description = description;
            Body = body;
            Slug = title.GenerateSlug();
            AuthorId = authorId;
        }
    }
}
