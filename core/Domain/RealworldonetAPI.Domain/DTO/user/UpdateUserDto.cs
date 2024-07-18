
namespace RealworldonetAPI.Domain.DTO.user
{
    public record UpdateUserDto
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }

        public static implicit operator UpdateUserDto(UpdatedUserDto v)
        {
            throw new NotImplementedException();
        }
    }
}
