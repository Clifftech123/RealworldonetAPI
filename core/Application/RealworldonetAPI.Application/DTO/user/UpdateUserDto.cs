namespace RealworldonetAPI.Domain.DTO.user
{
    public class UpdateUserWrapper
    {
        public UpdateUser User { get; set; }
    }

    public record class UpdateUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }
}

