using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RealworldonetAPI.Application.Commands.User
{
    /// <summary>
    /// Handles the update of a user.
    /// </summary>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/> class.
        /// </summary>
        /// <param name="userManager">The user manager for managing user accounts.</param>
        /// <param name="currentUser">The service for retrieving the current user.</param>
        public UpdateUserHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        /// <summary>
        /// Handles the update of a user.
        /// </summary>
        /// <param name="request">The command containing the user update details.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The details of the updated user.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user is not found.</exception>
        /// <exception cref="ValidationException">Thrown when the update fails or the password is invalid.</exception>
        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUser.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var updateUserDto = request.User.User;

            // Update email if provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Email))
            {
                user.Email = updateUserDto.Email ?? user.Email;
            }

            // Update username if provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Username))
            {
                user.UserName = updateUserDto.Username ?? user.UserName;
            }

            // Update bio if provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Bio))
            {
                user.Bio = updateUserDto.Bio;
            }

            // Update image if provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Image))
            {
                user.Image = updateUserDto.Image;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ValidationException($"Failed to update user: {errors}");
            }

            // Update password if provided
            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                if (!updateUserDto.Password.Any(char.IsDigit))
                {
                    throw new ValidationException("Password must include at least one number.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, updateUserDto.Password);
                if (!passwordResult.Succeeded)
                {
                    var passwordErrors = string.Join("; ", passwordResult.Errors.Select(e => e.Description));
                    throw new ValidationException($"Failed to update password: {passwordErrors}");
                }
            }

            return new UserDto
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                Image = user.Image ?? "https://api.realworld.io/images/smiley-cyrus.jpeg"
            };
        }
    }
}
