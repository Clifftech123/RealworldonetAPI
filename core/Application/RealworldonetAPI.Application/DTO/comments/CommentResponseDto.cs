using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.DTO.comments
{
    public record CommentResponseDto
    {
        public CommentDetailDto Comment { get; set; } = new CommentDetailDto();

        public class CommentDetailDto
        {
            public int Id { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
            public DateTimeOffset UpdatedAt { get; set; }
            public string Body { get; set; } = string.Empty;
            public AuthorDto Author { get; set; } = new AuthorDto();
        }

    }
}
