using Microsoft.AspNetCore.Identity;

namespace RealworldonetAPI.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Bio { get; set; } 
        public string? Image { get; set; } 
        public string? Token { get; set; } 
    }

}
