using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.DTO.article
{
    public class NewArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> TagList { get; set; }
    }

    public class UpdateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }

    public class ArticleResponseDto
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public List<string> TagList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Favorited { get; set; }
        public int FavoritesCount { get; set; }
        public ApplicationUser Author { get; set; }
    }

    

}




