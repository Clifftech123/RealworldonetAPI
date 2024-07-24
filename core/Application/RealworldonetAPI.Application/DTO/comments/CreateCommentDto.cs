namespace RealworldonetAPI.Application.DTO.comments
{
    public record CreateCommentDto
    {
        public CommentDto Comment { get; set; } = new CommentDto();

        public class CommentDto
        {
            public string Body { get; set; } = string.Empty;
        }
    }

}
