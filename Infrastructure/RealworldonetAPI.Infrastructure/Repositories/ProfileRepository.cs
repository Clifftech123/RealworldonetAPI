using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

public class ProfileRepository : IProfileRepository
{
    private readonly ApplicationDbContext _context;

    public ProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get a user's profile
    public async Task<UserProfile> GetProfileAsync(string username)
    {
        var user = await _context.Users
            .Where(u => u.UserName == username)
            .Select(u => new UserProfile
            {
                Username = u.UserName ?? string.Empty,
                Email = u.Email ?? string.Empty,
                Bio = u.Bio ?? string.Empty,
                Image = u.Image ?? string.Empty,
                Following = _context.UserLinks.Any(ul => ul.Username == username && ul.FollowerUsername == u.UserName)
            })
            .FirstOrDefaultAsync();

        return user ?? new UserProfile();
    }


    // Follow a user
    public async Task<bool> FollowUserAsync(string followerUsername, string followingUsername)
    {
        if (followerUsername == followingUsername)
        {
            return false;
        }

        // Check if the users exist
        var follower = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followerUsername);
        var following = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followingUsername);

        if (follower == null || following == null)
        {
            return false;
        }

        // Check if the user link already exists

        var userLinkExists = await _context.UserLinks.AnyAsync(ul => ul.Username == followingUsername && ul.FollowerUsername == followerUsername);

        if (userLinkExists)
        {
            return false;
        }

        _context.UserLinks.Add(new UserLink
        {
            Username = followingUsername,
            FollowerUsername = followerUsername
        });

        await _context.SaveChangesAsync();
        return true;
    }


    // Unfollow a user
    public async Task<bool> UnfollowUserAsync(string followerUsername, string followingUsername)
    {
        var userLink = await _context.UserLinks.FirstOrDefaultAsync(ul => ul.Username == followingUsername && ul.FollowerUsername == followerUsername);

        if (userLink == null)
        {
            return false;
        }

        _context.UserLinks.Remove(userLink);
        await _context.SaveChangesAsync();
        return true;
    }


}
