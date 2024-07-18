using MediatR;
using RealworldonetAPI.Domain.DTO.user;

namespace RealworldonetAPI.Application.Queries.User
{
    public class GetCurrentUserQuery : IRequest<UserDto>
    {

    }
}
