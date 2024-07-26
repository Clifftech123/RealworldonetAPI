using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Configurations
{
    /// <summary>
    /// Configures the Article entity.
    /// </summary>
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        /// <summary>
        /// Configures the Article entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.Body).IsRequired();

            // Predefined user for seeding
            var seedUser = new ApplicationUser
            {
                Id = "f3a8ec7c-ab34-4c89-a71b-fcbf9283f8e1",
                UserName = "seeduser@example.com",
                Email = "seeduser@example.com",
               
            };

            var articles = new List<Article>
            {
                new Article("First Global Article", "Description 1", "Body 1", seedUser.Id) { Id = Guid.NewGuid(), Slug = "first-global-article", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
                new Article("Second Global Article", "Description 2", "Body 2", seedUser.Id) { Id = Guid.NewGuid(), Slug = "second-global-article", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
                new Article("Third Global Article", "Description 3", "Body 3", seedUser.Id) { Id = Guid.NewGuid(), Slug = "third-global-article", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
                new Article("Fourth Global Article", "Description 4", "Body 4", seedUser.Id) { Id = Guid.NewGuid(), Slug = "fourth-global-article", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow },
                new Article("Fifth Global Article", "Description 5", "Body 5", seedUser.Id) { Id = Guid.NewGuid(), Slug = "fifth-global-article", CreatedAt = DateTimeOffset.UtcNow, UpdatedAt = DateTimeOffset.UtcNow }
            };

            builder.HasData(articles);

            builder.HasMany(a => a.Tags)
                .WithMany(t => t.Articles)
                .UsingEntity<Dictionary<string, object>>(
                    "ArticleTag",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId"),
                    j => j.HasOne<Article>().WithMany().HasForeignKey("ArticleId"),
                    j =>
                    {
                        j.HasKey("ArticleId", "TagId");
                        j.HasData(
                            new { ArticleId = articles[0].Id, TagId = 1 },
                            new { ArticleId = articles[0].Id, TagId = 2 },
                            new { ArticleId = articles[1].Id, TagId = 3 },
                            new { ArticleId = articles[2].Id, TagId = 4 },
                            new { ArticleId = articles[3].Id, TagId = 5 },
                            new { ArticleId = articles[4].Id, TagId = 1 }
                        );
                    }
                );
        }
    }
}
