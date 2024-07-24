using MediatR;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, bool>
    {
        private readonly IArticleRepository _articleRepository;

        public DeleteArticleHandler(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {

            var existingArticle = await _articleRepository.GetBySlugAsync(request.Slug);

            if (existingArticle == null)
            {
                throw new Exception("Artticel not found ");
            }
            await _articleRepository.DeleteBySlugAsync(request.Slug);
            return true;



        }
    }
}
