using MediatR;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{
    /// <summary>
    /// Handles the command to follow a user.
    /// </summary>
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowUserCommandHandler"/> class.
        /// </summary>
        /// <param name="profileRepository">The profile repository.</param>
        public FollowUserCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Handles the follow user command.
        /// </summary>
        /// <param name="request">The follow user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        public async Task<bool> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            return await _profileRepository.FollowUserAsync(request.FollowerUsername, request.FollowingUsername);
        }
    }
}
