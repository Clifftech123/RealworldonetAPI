using MediatR;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Application.Commands.Article
{
    /// <summary>
    /// Command to update an article.
    /// </summary>
    public class UpdateArticleCommand : IRequest<ArticleResponseDto>
    {
        /// <summary>
        /// Gets the slug of the article to be updated.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// Gets the data transfer object containing the updated article information.
        /// </summary>
        public UpdateArticleDto? UpdateArticle { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateArticleCommand"/> class.
        /// </summary>
        /// <param name="slug">The slug of the article to be updated.</param>
        /// <param name="updateArticle">The data transfer object containing the updated article information.</param>
        public UpdateArticleCommand(string slug, UpdateArticleDto updateArticle)
        {
            Slug = slug;
            UpdateArticle = updateArticle;
        }
    }
}
