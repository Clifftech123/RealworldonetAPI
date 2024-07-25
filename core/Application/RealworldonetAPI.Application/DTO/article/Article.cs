namespace RealworldonetAPI.Application.DTO.article
{


    public record NewArticleDto
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string Body { get; init; }
        public List<string> TagList { get; init; }
    }



    public record CreateArticleRequestDto
    {
        public NewArticleDto Article { get; init; }
    }



    public record UpdateArticleDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }


    public record ArticleResponseDto
    {
        public string Slug { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string Body { get; init; }
        public List<string> TagList { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset UpdatedAt { get; init; }
        public bool Favorited { get; init; }
        public int FavoritesCount { get; init; }
        public AuthorDto Author { get; init; }
    }


    public record AuthorDto
    {
        public string Username { get; init; }
        public string? Bio { get; init; }
        public string? Image { get; init; }
        public bool Following { get; init; }
    }


    public record ArticleResponseWrapper
    {
        public ArticleResponseDto Article { get; init; }
    }



    public record ArticlesResponseWrapper
    {
        public List<ArticleResponseDto> Articles { get; init; }
        public int ArticlesCount { get; init; }
       
    }



}




