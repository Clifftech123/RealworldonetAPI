using MediatR;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{

    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        public UnfollowUserCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<bool> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            return await _profileRepository.UnfollowUserAsync(request.FollowerUsername, request.FollowingUsername);
        }
    }
}
