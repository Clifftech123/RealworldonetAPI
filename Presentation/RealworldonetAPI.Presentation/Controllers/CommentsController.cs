using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Application.DTO.comments;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class CommentsController : BaseApiController
    {
        /// <summary>
        /// Gets an article by its slug.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <returns>An ArticleResponseDto containing the article details.</returns>
        [HttpGet("articles/{slug}/comments")]
        public async Task<ActionResult<CommentResponseWrapper>> GetCommentsBySlug(string slug)
        {
            return Ok(await Mediator.Send(new GetCommentCommand(slug)));
        }
        /// <summary>
        /// Creates a new comment for an article.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="newCommentDto">The details of the new comment.</param>
        /// <returns>A CommentResponseWrapper containing the created comment details.</returns>
        [HttpPost("articles/{slug}/comments")]
        public async Task<ActionResult<CommentResponseWrapper>> CreateComment(string slug, [FromBody] CreateCommentWrapper newCommentDto)
        {
            var command = new CreateCommentCommand(slug, newCommentDto);
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Deletes a comment from an article.
        /// </summary>
        /// <param name="slug">The slug of the article.</param>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        [HttpDelete("articles/{slug}/comments/{id}")]
        public async Task<ActionResult<bool>> DeleteComment(string slug, Guid id)
        {
            return Ok(await Mediator.Send(new DeleteCommentCommand(slug, id)));
        }
    }
}
