using MediatR;
using RealworldonetAPI.Application.DTO.comments;

namespace RealworldonetAPI.Application.Commands.Comments
{
     public class CreateCommentCommand : IRequest<CommentResponseDto>
    {
        public string Slug { get; }
        public CreateCommentDto? CreateComment { get; }

        public CreateCommentCommand(string slug, CreateCommentDto createComment)
        {
            Slug = slug;
            CreateComment = createComment;
        }
    }
}
