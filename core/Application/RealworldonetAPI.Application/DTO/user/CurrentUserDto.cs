namespace RealworldonetAPI.Domain.DTO.user
{
    public record CurrentUserDto
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Image { get; set; }
    }
}
