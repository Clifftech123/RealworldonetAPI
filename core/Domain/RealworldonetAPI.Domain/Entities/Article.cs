namespace RealworldonetAPI.Domain.Entities
{
    public class Article
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> TagList { get; set; } = new List<string>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; }
        public ApplicationUser Author { get; set; }
        public string AuthorId { get; set; }
    }
    
   

}
