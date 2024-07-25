using MediatR;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    /// <summary>
    /// Handles the deletion of an article.
    /// </summary>
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteArticleHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The repository for managing articles.</param>
        public DeleteArticleHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        /// <summary>
        /// Handles the deletion of an article.
        /// </summary>
        /// <param name="request">The command containing the details of the article to delete.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        /// <exception cref="Exception">Thrown when the article is not found.</exception>
        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var existingArticle = await _articleRepository.GetBySlugAsync(request.Slug);

            if (existingArticle == null)
            {
                throw new Exception("Article not found");
            }

            await _articleRepository.DeleteBySlugAsync(request.Slug);
            return true;
        }
    }
}
