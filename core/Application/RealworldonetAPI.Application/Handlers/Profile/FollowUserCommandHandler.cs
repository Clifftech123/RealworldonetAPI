using MediatR;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{

    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        public FollowUserCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<bool> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            return await _profileRepository.FollowUserAsync(request.FollowerUsername, request.FollowingUsername);
        }
    }
}
