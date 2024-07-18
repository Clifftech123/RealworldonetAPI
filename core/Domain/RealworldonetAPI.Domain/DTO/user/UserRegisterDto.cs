namespace RealworldonetAPI.Domain.DTO.user
{
    public record UserRegisterDto
    {

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
