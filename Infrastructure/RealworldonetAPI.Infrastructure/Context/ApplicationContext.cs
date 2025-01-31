﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ArticleFavorite> ArticleFavorites { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasMany(x => x.ArticleComments)
                    .WithOne(x => x.Author);

                entity.HasMany(e => e.Followers)
                    .WithMany(e => e.FollowedUsers)
                    .UsingEntity(j => j.ToTable("UserFollowers"));
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Slug).IsUnique();
                //entity.Ignore(e => e.Favorited);
                entity.Ignore(e => e.FavoritesCount);
                entity.HasOne(a => a.Author)
                    .WithMany(a => a.Articles)
                    .HasForeignKey(a => a.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ArticleFavorite>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.UserId });
                entity.HasOne(x => x.Article)
                    .WithMany(x => x.ArticleFavorites)
                    .HasForeignKey(x => x.ArticleId);
                entity.HasOne(x => x.User)
                    .WithMany(x => x.ArticleFavorites)
                    .HasForeignKey(x => x.UserId);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(x => x.Article)
                    .WithMany(x => x.Comments)
                    .HasForeignKey(x => x.ArticleId);
                entity.HasOne(x => x.Author)
                    .WithMany(x => x.ArticleComments)
                    .HasForeignKey(c => c.AuthorId);
            });
        }
    }
}
