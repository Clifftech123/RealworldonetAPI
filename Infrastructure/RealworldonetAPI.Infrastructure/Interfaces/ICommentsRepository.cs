using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Infrastructure.Interfaces
{

    public interface ICommentsRepository
    {
        Task<IEnumerable<Comment>> GetCommentsForArticleAsync(string slug);
        Task<Comment> CreateCommentForArticleAsync(string slug, Comment comment);
        Task<bool> DeleteCommentForArticleAsync(string slug, Guid commentId);
        Task<Comment?> GetCommentByIdAsync(Guid commentId);
    }
}
