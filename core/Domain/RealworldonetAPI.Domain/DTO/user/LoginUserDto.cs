namespace RealworldonetAPI.Domain.DTO.user
{
    public record LoginUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
