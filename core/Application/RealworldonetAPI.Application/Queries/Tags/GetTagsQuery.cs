using MediatR;

namespace RealworldonetAPI.Application.Queries.Tags
{
    public class GetTagsQuery : IRequest<List<string>> { }
}
