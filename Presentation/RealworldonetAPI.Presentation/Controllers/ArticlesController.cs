using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Domain.models;

namespace RealworldonetAPI.Presentation.Controllers
{
    /// <summary>
    /// Controller for managing articles.
    /// </summary>
    public class ArticlesController : BaseApiController
    {
        /// <summary>
        /// Gets the article feed for the current user.
        /// </summary>
        /// <param name="offset">The offset for pagination.</param>
        /// <param name="limit">The limit for pagination.</param>
        /// <returns>The article feed for the current user.</returns>
        [HttpGet("article/feed")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetArticleFeed([FromQuery] int offset = 1, [FromQuery] int limit = 20)
        {
            return Ok(await Mediator.Send(new GetArticlesFeedQuery(offset, limit)));
        }

        /// <summary>
        /// Gets the global article feed.
        /// </summary>
        /// <param name="tag">The tag to filter articles by.</param>
        /// <param name="author">The author to filter articles by.</param>
        /// <param name="favorited">The user who favorited the articles to filter by.</param>
        /// <param name="offset">The offset for pagination.</param>
        /// <param name="limit">The limit for pagination.</param>
        /// <returns>The global article feed.</returns>
        [HttpGet("article")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetArticleGlobalFeed([FromQuery] string? tag = null, [FromQuery] string? author = null, [FromQuery] string? favorited = null, [FromQuery] int offset = 1, [FromQuery] int limit = 20)
        {
            return Ok(await Mediator.Send(new GetGlobalArticlesQuery(tag, author, favorited, offset, limit)));
        }

        /// <summary>
        /// Creates a new article.
        /// </summary>
        /// <param name="request">The article creation request.</param>
        /// <returns>The created article.</returns>
        [HttpPost("article")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ArticleResponseWrapper>> CreateArticle([FromBody] CreateArticleRequestDto request)
        {
            var command = new CreateArticleCommand(request.Article);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Gets an article by its slug.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <returns>The article with the specified slug.</returns>
        [HttpGet("article/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetArticleBySlug(string slug)
        {
            return Ok(await Mediator.Send(new GetArticleBySlugQuery(slug)));
        }

        /// <summary>
        /// Updates an article by its slug.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="updateArticleDto">The article update details.</param>
        /// <returns>The updated article.</returns>
        [HttpPut("article/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateArticle(string slug, [FromBody] UpdateArticleDto updateArticleDto)
        {
            return Ok(await Mediator.Send(new UpdateArticleCommand(slug, updateArticleDto)));
        }

        /// <summary>
        /// Deletes an article by its slug.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <returns>A confirmation of the article deletion.</returns>
        [HttpDelete("article/{slug}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteArticle(string slug)
        {
            return Ok(await Mediator.Send(new DeleteArticleCommand(slug)));
        }
    }
}
