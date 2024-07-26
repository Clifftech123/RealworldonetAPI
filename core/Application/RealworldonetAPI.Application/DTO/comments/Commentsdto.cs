using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.DTO.comments
{
    public record CreateCommentDto
    {
        public string Body { get; set; } = string.Empty;
    }

    public record CreateCommentWrapper
    {
        public CreateCommentDto Comment { get; set; } = new CreateCommentDto();
    }

    public record CommentResponseWrapper
    {
        public CommentDetailDto Comment { get; set; }
    }

    public record CommentDetailDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string Body { get; set; } = string.Empty;
        public AuthorDto Author { get; set; } = new AuthorDto();
    }
}
