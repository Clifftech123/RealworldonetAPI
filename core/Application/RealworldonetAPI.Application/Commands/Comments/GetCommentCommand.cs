using MediatR;
using RealworldonetAPI.Application.DTO.comments;

namespace RealworldonetAPI.Application.Commands.Comments
{
    public class GetCommentCommand : IRequest<List<CommentDetailDto>>
    {
        public string Slug { get; }

        public GetCommentCommand(string slug)
        {
            Slug = slug;
        }
    }
}
