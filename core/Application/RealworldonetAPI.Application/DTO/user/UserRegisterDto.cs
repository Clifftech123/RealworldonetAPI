
namespace RealworldonetAPI.Domain.DTO.user
{
    namespace RealworldonetAPI.Domain.DTO.user
    {
        public class UserRegisterWrapper
        {
            public NewUser User { get; set; }
        }

        public record NewUser
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }



}
