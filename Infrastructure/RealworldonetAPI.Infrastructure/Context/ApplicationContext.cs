using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
namespace RealworldonetAPI.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<UserLink> UserLinks { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Article-Author relationship
            builder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId);

            // UserLink relationships
            builder.Entity<UserLink>()
                .HasKey(ul => new { ul.Username, ul.FollowerUsername }); 

            builder.Entity<UserLink>()
                .HasOne(ul => ul.User)
                .WithMany()
                .HasForeignKey(ul => ul.Username)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Entity<UserLink>()
                .HasOne(ul => ul.FollowerUser)
                .WithMany()
                .HasForeignKey(ul => ul.FollowerUsername)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
