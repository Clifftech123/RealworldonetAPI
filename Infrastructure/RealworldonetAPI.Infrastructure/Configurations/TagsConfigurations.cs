using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Configurations
{
    public class TagsConfigurations : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired();

            // Seed initial tags
            builder.HasData(
                new Tag { Id = 1, Name = "eos" },
                new Tag { Id = 2, Name = "est" },
                new Tag { Id = 3, Name = "ipsum" },
                new Tag { Id = 4, Name = "enim" },
                new Tag { Id = 5, Name = "repellat" },
                new Tag { Id = 6, Name = "exercitationem" },
                new Tag { Id = 7, Name = "quia" },
                new Tag { Id = 8, Name = "consequatur" },
                new Tag { Id = 9, Name = "facilis" },
                new Tag { Id = 10, Name = "tenetur" }
            );
        }
    }
}
