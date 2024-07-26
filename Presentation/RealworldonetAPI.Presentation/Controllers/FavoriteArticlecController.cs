using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;

namespace RealworldonetAPI.Presentation.Controllers
{
    
    public class FavoritesController : BaseApiController
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator instance.</param>
        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Favorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to be favorited.</param>
        /// <returns>The response DTO containing the favorited article information.</returns>
        [HttpPost("articles/{slug}/favorite")]
        public async Task<ActionResult<ArticleResponseDto>> FavoriteArticle(string slug)
        {
            var result = await _mediator.Send(new FavoriteArticleCommand(slug));
            return Ok(result);
        }

        /// <summary>
        /// Unfavorites an article.
        /// </summary>
        /// <param name="slug">The slug of the article to be unfavorited.</param>
        /// <returns>The response DTO containing the unfavorited article information.</returns>
        [HttpDelete("articles/{slug}/favorite")]
        public async Task<ActionResult<ArticleResponseDto>> UnfavoriteArticle(string slug)
        {
            var result = await _mediator.Send(new UnfavoriteArticleCommand(slug));
            return Ok(result);
        }
    }
}
