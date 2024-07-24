using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.DTO.comments;
using RealworldonetAPI.Application.Queries.Article;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class CommentsController : BaseApiController
    {




        [HttpGet("article/slug")]


        public async Task<ActionResult<ArticleResponseDto>> GetArticleBySlug(string slug)
        {
            return Ok(await Mediator.Send(new GetArticleBySlugQuery(slug)));
        }


        [HttpPost("articles/{slug}/comments")]
        public async Task<ActionResult<CommentResponseDto>> CreateComment(string slug, [FromBody] CreateCommentDto newCommentDto)
        {
            var command = new CreateCommentCommand(slug, newCommentDto);
            return Ok(await Mediator.Send(command));
        }


        [HttpDelete("articles/{slug}/comments/{id}")]
        public async Task<ActionResult<bool>> DeleteComment(string slug, Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCommentCommand(slug, id)));
        }





    }
}
