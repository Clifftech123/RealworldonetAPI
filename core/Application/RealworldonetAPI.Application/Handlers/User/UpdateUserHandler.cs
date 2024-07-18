using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Application.Commands.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        // Update user details and return the updated user details
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Update user details

            var updateUserDto = request.User;
            user.Email = updateUserDto.Email;
            user.UserName = updateUserDto.Username;
            user.Bio = updateUserDto.Bio;
            user.Image = updateUserDto.Image;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to update user.");
            }

            // Update password if it's provided in the request
            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);
                if (!passwordResult.Succeeded)
                {
                    throw new Exception("Failed to update password.");
                }
            }

            return new UserDto
            {
                UserName = user.UserName,
                UserEmail = user.Email,
                Email = user.Email,
            };

        }


    }
}
