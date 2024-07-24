namespace RealworldonetAPI.Domain.Entities
{
    public class UserLink
    {
        public string Username { get; set; } = null!;
        public string FollowerUsername { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public ApplicationUser FollowerUser { get; set; } = null!;
    }

}
