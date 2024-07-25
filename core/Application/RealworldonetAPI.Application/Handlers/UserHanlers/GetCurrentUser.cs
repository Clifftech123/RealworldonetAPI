using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Queries.User
{
    /// <summary>  
    /// Handles the retrieval of the current user.  
    /// </summary>  
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, object>
    {

        /// <summary>  
        /// Initializes a new instance of the <see cref="GetCurrentUserQueryHandler"/> class.  
        /// </summary>  
        /// <param name="userManager">The user manager for managing user accounts.</param>  
        /// <param name="currentUser">The service for retrieving the current user.</param>  
        /// <param name="tokenService">The service for creating tokens.</param>  
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;
        private readonly ITokenService _tokenService;

        /// <summary>  
        /// Handles the retrieval of the current user.  
        /// </summary>  
        /// <param name="request">The query containing the details for retrieving the current user.</param>  
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>  
        /// <returns>An object containing the details of the current user.</returns>  
        /// <exception cref="Exception">Thrown when the user is not found.</exception>  

        public GetCurrentUserQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser, ITokenService tokenService)
        {
            _userManager = userManager;
            _currentUser = currentUser;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception($"User with {user} not found.");
            }

            var token = await _tokenService.CreateToken(user);


            var response = new
            {
                user = new
                {
                    id = user.Id,
                    email = user.Email,
                    username = user.UserName,
                    bio = user.Bio,
                    image = user.Image,
                    token = token
                }
            };

            return response;
        }
    }
}
