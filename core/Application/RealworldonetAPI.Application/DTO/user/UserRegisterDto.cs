
namespace RealworldonetAPI.Domain.DTO.user
{
    namespace RealworldonetAPI.Domain.DTO.user
    {
        public class UserRegisterWrapper
        {
            public UserRegisterDto User { get; set; }
        }

        public record UserRegisterDto
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }



}
