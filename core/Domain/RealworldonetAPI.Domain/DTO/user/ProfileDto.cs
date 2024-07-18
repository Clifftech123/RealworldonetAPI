namespace RealworldonetAPI.Domain.DTO.user
{
    public record ProfileDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
