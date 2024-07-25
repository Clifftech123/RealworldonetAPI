
using MediatR;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    /// <summary>  
    /// Handles the deletion of a comment for a specific article.  
    /// </summary>  
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentsRepository _commentRepository;

        /// <summary>  
        /// Initializes a new instance of the <see cref="DeleteCommentCommandHandler"/> class.  
        /// </summary>  
        /// <param name="commentRepository">The repository for managing comments.</param>  
        public DeleteCommentCommandHandler(ICommentsRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        /// <summary>  
        /// Handles the deletion of a comment for a specific article.  
        /// </summary>  
        /// <param name="request">The command containing the details of the comment to delete.</param>  
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
        /// <returns>A boolean indicating whether the deletion was successful.</returns>  
        /// <exception cref="KeyNotFoundException">Thrown when the comment is not found.</exception>  
        /// <exception cref="InvalidOperationException">Thrown when an error occurs during deletion.</exception>  
        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var existingComment = await _commentRepository.GetCommentByIdAsync(request.CommentId);

            if (existingComment == null)
            {
                throw new KeyNotFoundException("Comment not found");
            }

            try
            {
                await _commentRepository.DeleteCommentForArticleAsync(request.Slug, request.CommentId);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting the comment: {ex.Message}", ex);
            }
        }
    }
}
