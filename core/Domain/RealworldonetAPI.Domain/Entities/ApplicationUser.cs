using Microsoft.AspNetCore.Identity;

namespace RealworldonetAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Bio { get; set; }
        public string? Image { get; set; }
        public string? Token { get; set; }

        public ICollection<ApplicationUser> Followers { get; set; }
        public ICollection<ApplicationUser> FollowedUsers { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<ArticleFavorite>? ArticleFavorites { get; set; }
        public ICollection<Comment>? ArticleComments { get; set; }
    }
}
