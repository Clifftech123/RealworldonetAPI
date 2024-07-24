using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();

            builder.HasData(
                new Tag { Id = 1, Name = "programming" },
                new Tag { Id = 2, Name = "javascript" },
                new Tag { Id = 3, Name = "react" },
                new Tag { Id = 4, Name = "angular" },
                new Tag { Id = 5, Name = "vue" }
            );
        }
    }

}
