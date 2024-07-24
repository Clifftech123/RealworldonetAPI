using Microsoft.AspNetCore.Identity;

namespace RealworldonetAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public object following;

        public string? Bio { get; set; } 
        public string? Image { get; set; } 
        public string? Token { get; set; }

        public ICollection<UserLink> Followers { get; set; } = new List<UserLink>();

        public ICollection<UserLink> FollowedUsers { get; set; } = new List<UserLink>();

         public ICollection<Article> Articles { get; set; } =  new List<Article>();

        public ICollection<ArticleFavorite>? ArticleFavorites { get; set; }
        public ICollection<Comment>? ArticleComments { get; set; }

    }

}
