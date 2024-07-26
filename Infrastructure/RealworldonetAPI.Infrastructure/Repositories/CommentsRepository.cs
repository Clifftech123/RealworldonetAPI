using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing comments in the database.
    /// </summary>
    public class CommentsRepository : ICommentsRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommentsRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the context is null.</exception>
        public CommentsRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Creates a new comment for an article asynchronously.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="comment">The comment to create.</param>
        /// <returns>The created comment.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an error creating the comment.</exception>
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

        /// <summary>
        /// Retrieves comments for an article by its slug asynchronously.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <returns>A list of comments for the article.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an error retrieving the comments.</exception>
        public async Task<IEnumerable<Comment>> GetCommentsForArticleAsync(string slug)
        {
            try
            {
                return await _context.Comments.Where(c => c.Article.Slug == slug).Where(c => c.ArticleId == c.Article.Id).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving comments for article with slug {slug}", ex);
            }
        }

        /// <summary>
        /// Deletes a comment from an article asynchronously.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="commentId">The ID of the comment to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an error deleting the comment.</exception>
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

        /// <summary>
        /// Retrieves a comment by its ID asynchronously.
        /// </summary>
        /// <param name="commentId">The ID of the comment.</param>
        /// <returns>The comment with the specified ID.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is an error retrieving the comment.</exception>
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
