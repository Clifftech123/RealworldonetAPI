namespace RealworldonetAPI.Domain.Entities
{
    public class UserLink
    {
        public string Username { get; set; }
        public string FollowerUsername { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser FollowerUser { get; set; }
    }

}
