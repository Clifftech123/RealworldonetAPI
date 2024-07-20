
namespace RealworldonetAPI.Domain.DTO.user
{
    namespace RealworldonetAPI.Domain.DTO.user
    {
        public class RegistrationResponse
        {
            public UserResponse User { get; set; }
            public Dictionary<string, string[]> Errors { get; set; }
        }

        public class UserResponse
        {

            public string Email { get; set; }
            public string Token { get; set; }
            public string Username { get; set; }
            public string Bio { get; set; } = string.Empty;
            public string Image { get; set; } = "https://api.realworld.io/images/smiley-cyrus.jpeg";
        }

    }
}
