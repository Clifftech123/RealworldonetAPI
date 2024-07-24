using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser
                {
                    Id = "f3a8ec7c-ab34-4c89-a71b-fcbf9283f8e1",
                    UserName = "seeduser@example.com",
                    Email = "seeduser@example.com",

                });
            /*  builder.HasData(
                  new ApplicationUser
                  {
                      Id = "user1",
                      UserName = "johndoe",
                      Email = "johndoe@example.com",
                      Bio = "I'm a software developer",
                      Image = "https://api.realworld.io/images/demo-avatar.png"
                  },
                  new ApplicationUser
                  {
                      Id = "user2",
                      UserName = "janedoe",
                      Email = "janedoe@example.com",
                      Bio = "I'm a web designer",
                      Image = "https://api.realworld.io/images/demo-avatar.png"
                  }
              ); */
        }
    }

}
