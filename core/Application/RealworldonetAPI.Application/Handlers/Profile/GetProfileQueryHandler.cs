using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Application.Queries.Profile;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Profile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, UserProfiledto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetProfileQueryHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<UserProfiledto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _profileRepository.GetProfileAsync(request.Username);
            return _mapper.Map<UserProfiledto>(userProfile);
        }
    }

}
