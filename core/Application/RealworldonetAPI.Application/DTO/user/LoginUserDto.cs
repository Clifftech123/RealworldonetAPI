namespace RealworldonetAPI.Domain.DTO.user
{
    public class LoginUserWrapper
    {
        public LoginUserDto User { get; set; }
    }

    public record LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
