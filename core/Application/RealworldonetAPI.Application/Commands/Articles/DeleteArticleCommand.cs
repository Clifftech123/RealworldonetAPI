using MediatR;

namespace RealworldonetAPI.Application.Commands.Article
{
    /// <summary>
    /// Command to delete an article.
    /// </summary>
    /// <param name="Slug">The slug of the article to be deleted.</param>
    public record DeleteArticleCommand(string Slug) : IRequest<bool>;
}
