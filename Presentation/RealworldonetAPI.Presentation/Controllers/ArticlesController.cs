using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class ArticlesController : BaseApiController
    {

        // Get article  feed 

        [HttpGet(" article/feed")]
        [Authorize]
        public async Task<IActionResult> GetArticleFeed([FromQuery] int offset = 1, [FromQuery] int limit = 20)
        {
            return Ok(await Mediator.Send(new GetArticlesFeedQuery(offset, limit)));
        }

        [HttpGet("article")]
        public async Task<IActionResult> GetArticleGlobalFeed([FromQuery] string? tag = null, [FromQuery] string? author = null, [FromQuery] string? favorited = null, [FromQuery] int offset = 1, [FromQuery] int limit = 20)
        {
            return Ok(await Mediator.Send(new GetGlobalArticlesQuery(tag, author, favorited, offset, limit)));
        }


        [HttpPost(" article")]
        public async Task<ActionResult<ArticleResponseWrapper>> CreateArticle([FromBody] CreateArticleRequestDto request)
        {
            var command = new CreateArticleCommand(request.Article);
            var result = await Mediator.Send(command);
            return Ok(result);
        }


        // Get article by slug

        [HttpGet(" article/{slug}")]
        public async Task<IActionResult> GetArticleBySlug(string slug)
        {
            return Ok(await Mediator.Send(new GetArticleBySlugQuery(slug)));
        }


        [HttpPut(" article/{slug}")]

        public async Task<IActionResult> UpdateArticle(string slug, [FromBody] UpdateArticleDto updateArticleDto)
        {
            return Ok(await Mediator.Send(new UpdateArticleCommand(slug, updateArticleDto)));
        }


        [HttpDelete(" article/{slug}")]
        public async Task<IActionResult> DeleteArticle(string slug)
        {
            return Ok(await Mediator.Send(new DeleteArticleCommand(slug)));
        }






    }
}

