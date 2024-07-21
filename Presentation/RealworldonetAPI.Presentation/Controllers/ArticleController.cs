using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Article;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class ArticleController : BaseApiController
    {

        // Get article  feed 

        [HttpGet(" article/feed")]

        public async Task<IActionResult> GetArticleFeed([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await Mediator.Send(new GetArticlesFeedQuery(pageNumber, pageSize)));
        }



        [HttpGet(" article")]
        public async Task<IActionResult> GetArticeGobalFeed([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await Mediator.Send(new GetGlobalArticlesQuery(pageNumber, pageSize)));
        }


        [HttpPost(" article")]

        public async Task<IActionResult> CreateArticle([FromBody] NewArticleDto newArticleDto)
        {
            return Ok(await Mediator.Send(new CreateArticleCommand(newArticleDto)));
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

