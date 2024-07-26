using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Application.Queries.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{
    /// <summary>
    /// Handles the query to get a user profile.
    /// </summary>
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserProfiledto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileQueryHandler"/> class.
        /// </summary>
        /// <param name="profileRepository">The profile repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public GetProfileQueryHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to get a user profile.
        /// </summary>
        /// <param name="request">The query request containing the username.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user profile DTO.</returns>
        public async Task<UserProfiledto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _profileRepository.GetProfileAsync(request.Username);
            return _mapper.Map<UserProfiledto>(userProfile);
        }
    }
}
