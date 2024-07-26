namespace RealworldonetAPI.Application.DTO.user
{

    public class LoginUserWrapper
    {
        public LoginUser User { get; set; }
    }

    public record LoginUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
