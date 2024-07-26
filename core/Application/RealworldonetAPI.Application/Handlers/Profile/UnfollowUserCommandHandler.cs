using MediatR;
using RealworldonetAPI.Application.Commands.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{
    /// <summary>
    /// Handles the command to unfollow a user.
    /// </summary>
    public class UnfollowUserCommandHandler : IRequestHandler<UnfollowUserCommand, bool>
    {
        private readonly IProfileRepository _profileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnfollowUserCommandHandler"/> class.
        /// </summary>
        /// <param name="profileRepository">The profile repository.</param>
        public UnfollowUserCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        /// <summary>
        /// Handles the unfollow user command.
        /// </summary>
        /// <param name="request">The unfollow user command request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the operation was successful.</returns>
        public async Task<bool> Handle(UnfollowUserCommand request, CancellationToken cancellationToken)
        {
            return await _profileRepository.UnfollowUserAsync(request.FollowerUsername, request.FollowingUsername);
        }
    }
}
