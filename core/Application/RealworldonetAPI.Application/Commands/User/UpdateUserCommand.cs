using MediatR;
using RealworldonetAPI.Domain.DTO.user;

namespace RealworldonetAPI.Application.Commands.User
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public UpdateUserDto User { get; set; }
    }

}
