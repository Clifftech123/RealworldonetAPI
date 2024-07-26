using MediatR;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.Queries.Tags;
using RealworldonetAPI.Infrastructure.Context;

namespace RealworldonetAPI.Application.Handlers.Tag
{
    /// <summary>
    /// Handles the query to get a list of tags.
    /// </summary>
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<string>>
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetTagsQueryHandler"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public GetTagsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the query to get a list of tags.
        /// </summary>
        /// <param name="request">The query request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of tag names.</returns>
        public async Task<List<string>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tags.Select(t => t.Name).ToListAsync(cancellationToken);
        }
    }
}
