using MediatR;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentsRepository _commentRepository;

        public DeleteCommentCommandHandler(ICommentsRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

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
