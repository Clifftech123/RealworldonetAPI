using MediatR;

namespace RealworldonetAPI.Application.Commands.Comments
{
    public class DeleteCommentCommand : IRequest<bool>
    {
        public string Slug { get; }
        public Guid CommentId { get; }

        public DeleteCommentCommand(string slug, Guid commentId)
        {
            Slug = slug;
            CommentId = commentId;
        }
    }
}
