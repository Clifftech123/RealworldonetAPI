using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Comment> CreateCommentForArticleAsync(string slug, Comment comment)
        {
            try
            {
                var article = await _context.Articles.FirstOrDefaultAsync(a => a.Slug == slug);
                if (article == null)
                {
                    throw new InvalidOperationException("Article not found");
                }

                comment.ArticleId = article.Id;

                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return comment;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error creating comment for article", ex);
            }
        }


        public async Task<IEnumerable<Comment>> GetCommentsForArticleAsync(string slug)
        {
            try
            {
                return await _context.Comments.Where(c => c.Article.Slug == slug).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving comments for article with slug {slug}", ex);
            }
        }

        public async Task<bool> DeleteCommentForArticleAsync(string slug, Guid commentId)
        {
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Article.Slug == slug && c.Id == commentId);
                if (comment == null)
                {
                    return false;
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting comment with ID {commentId} for article with slug {slug}", ex);
            }
        }

        public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
        {
            try
            {
                return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving comment with ID {commentId}", ex);
            }
        }
    }
}
