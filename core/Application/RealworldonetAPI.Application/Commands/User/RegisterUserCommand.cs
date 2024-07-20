using MediatR;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.DTO.user.RealworldonetAPI.Domain.DTO.user;

namespace RealworldonetAPI.Application.Commands.User
{
    public class RegisterUserCommand : IRequest<UserDto>
    {
        public UserRegisterWrapper? userdto { get; set; }
    }
}
