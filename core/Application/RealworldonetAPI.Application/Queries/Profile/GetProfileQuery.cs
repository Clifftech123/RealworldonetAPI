using MediatR;
using RealworldonetAPI.Application.DTO.user;

namespace RealworldonetAPI.Application.Queries.Profile
{
    public class GetProfileQuery : IRequest<UserProfiledto>
    {
        public string Username { get; }

        public GetProfileQuery(string username)
        {
            Username = username;
        }
    }
}
