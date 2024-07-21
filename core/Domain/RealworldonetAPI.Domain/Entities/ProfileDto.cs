namespace RealworldonetAPI.Application.DTO.user
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Bio { get; set; }
        public bool Following { get; set; }
    }
}
