using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class FavoritesController : BaseApiController
    {

        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("articles/{slug}/favorite")]
        public async Task<ActionResult<ArticleResponseDto>> FavoriteArticle(string slug)
        {
            return await _mediator.Send(new FavoriteArticleCommand(slug));
        }

        [HttpDelete("articles/{slug}/favorite")]
        public async Task<ActionResult<ArticleResponseDto>> UnfavoriteArticle(string slug)
        {
            return await _mediator.Send(new UnfavoriteArticleCommand(slug));
        }

    }
}
