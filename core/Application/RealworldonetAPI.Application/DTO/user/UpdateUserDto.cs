namespace RealworldonetAPI.Domain.DTO.user
{
    public class UpdateUserWrapper
    {
        public UpdateUserDto User { get; set; }
    }

    public record UpdateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
    }
}

