using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Article
{
    /// <summary>
    /// Command to create a new article.
    /// </summary>
    public class CreateArticleCommand : IRequest<ArticleResponseDto>
    {
        /// <summary>
        /// Gets the data transfer object containing the new article information.
        /// </summary>
        public NewArticleDto CreateArticle { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateArticleCommand"/> class.
        /// </summary>
        /// <param name="createArticle">The data transfer object containing the new article information.</param>
        public CreateArticleCommand(NewArticleDto createArticle)
        {
            CreateArticle = createArticle ?? throw new ArgumentNullException(nameof(createArticle));
        }
    }
}
