namespace RealworldonetAPI.Domain.DTO.user
{
    public record UpdatedUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Password { get; set; }
    }
}
