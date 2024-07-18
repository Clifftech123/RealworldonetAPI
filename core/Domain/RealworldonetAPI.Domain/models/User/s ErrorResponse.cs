namespace RealworldonetAPI.Domain.models.User
{
    public class ErrorResponse
    {
        public ErrorModel Errors { get; set; }
    }

    public class ErrorModel
    {
        public List<string> Body { get; set; }
    }


}
