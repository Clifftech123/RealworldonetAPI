using MediatR;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.Queries.Tags;
using RealworldonetAPI.Infrastructure.Context;

namespace RealworldonetAPI.Application.Handlers.Tag
{

    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<string>>
    {
        private readonly ApplicationDbContext _context;

        public GetTagsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Tags.Select(t => t.Name).ToListAsync(cancellationToken);
        }
    }

}
