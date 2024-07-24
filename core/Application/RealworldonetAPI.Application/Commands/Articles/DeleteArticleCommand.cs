using MediatR;

namespace RealworldonetAPI.Application.Commands.Article
{
    public record DeleteArticleCommand(string Slug) : IRequest<bool>;
}
