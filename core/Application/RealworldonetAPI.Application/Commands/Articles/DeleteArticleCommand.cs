using MediatR;

namespace RealworldonetAPI.Application.Commands.Article
{
    public class DeleteArticleCommand : IRequest<bool>
    {

        public string Slug { get; }

        public DeleteArticleCommand(string slug)
        {
            Slug = slug;
        }

    }
}
