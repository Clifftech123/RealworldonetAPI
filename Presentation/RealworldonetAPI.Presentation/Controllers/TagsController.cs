using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealworldonetAPI.Application.Queries.Tags;
using RealworldonetAPI.Domain.models;

namespace RealworldonetAPI.Presentation.Controllers
{
    public class TagsController : BaseApiController
    {

        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // Get all tags from the database

        [HttpGet("tags")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> Get()
        {
            var tags = await _mediator.Send(new GetTagsQuery());
            return Ok(new { tags });
        }
    }
}
