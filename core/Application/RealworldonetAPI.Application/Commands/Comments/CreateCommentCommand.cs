using MediatR;
using RealworldonetAPI.Application.DTO.comments;

public class CreateCommentCommand : IRequest<CommentDetailDto>
{
    public string Slug { get; }
    public CreateCommentWrapper Comment { get; }

    public CreateCommentCommand(string slug, CreateCommentWrapper comment)
    {
        Slug = slug;
        Comment = comment ?? throw new ArgumentNullException(nameof(comment));
    }
}
