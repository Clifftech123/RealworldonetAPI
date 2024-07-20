namespace RealworldonetAPI.Domain.DTO.user
{

    public record UserDto
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}
