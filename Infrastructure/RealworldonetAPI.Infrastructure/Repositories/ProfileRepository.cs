using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealworldonetAPI.Application.DTO.user;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

public class ProfileRepository : IProfileRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ProfileRepository> _logger;

    public ProfileRepository(ApplicationDbContext context, IMapper mapper, ILogger<ProfileRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserProfiledto> GetProfileAsync(string username)
    {
        var user = await _context.Users
            .Include(u => u.Followers)
            .Include(u => u.FollowedUsers)
            .Include(u => u.Articles)
            .FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null)
        {
            _logger.LogWarning("Profile not found for username: {Username}", username);
            return null;
        }

        var userProfile = _mapper.Map<UserProfiledto>(user);
        userProfile.Following = user.Followers.Any(f => f.UserName == username);

        _logger.LogInformation("Profile retrieved for username: {Username}", username);
        return userProfile;
    }

    public async Task<bool> FollowUserAsync(string followerUsername, string followingUsername)
    {
        try
        {
            if (followerUsername == followingUsername)
            {
                _logger.LogWarning("Attempt to follow self: {FollowerUsername}", followerUsername);
                return false;
            }

            var follower = await _context.Users
                .Include(u => u.FollowedUsers)
                .FirstOrDefaultAsync(u => u.UserName == followerUsername);
            var following = await _context.Users.FirstOrDefaultAsync(u => u.UserName == followingUsername);

            if (follower == null || following == null)
            {
                _logger.LogWarning("Follow attempt failed. Follower: {FollowerUsername}, Following: {FollowingUsername}. One or both users not found.", followerUsername, followingUsername);
                return false;
            }

            if (follower.FollowedUsers.Any(u => u.Id == following.Id))
            {
                _logger.LogInformation("{FollowerUsername} is already following {FollowingUsername}", followerUsername, followingUsername);
                return false;
            }

            follower.FollowedUsers.Add(following);
            await _context.SaveChangesAsync();

            _logger.LogInformation("{FollowerUsername} successfully followed {FollowingUsername}", followerUsername, followingUsername);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in FollowUserAsync. Follower: {FollowerUsername}, Following: {FollowingUsername}", followerUsername, followingUsername);
            return false;
        }
    }

    public async Task<bool> UnfollowUserAsync(string followerUsername, string followingUsername)
    {
        try
        {
            var follower = await _context.Users
                .Include(u => u.FollowedUsers)
                .FirstOrDefaultAsync(u => u.UserName == followerUsername);
            var following = await _context.Users
                .Include(u => u.Followers)
                .FirstOrDefaultAsync(u => u.UserName == followingUsername);

            if (follower == null || following == null)
            {
                _logger.LogWarning("Unfollow attempt failed. Follower: {FollowerUsername}, Following: {FollowingUsername}. One or both users not found.", followerUsername, followingUsername);
                return false;
            }

            var followRelation = follower.FollowedUsers.FirstOrDefault(u => u.Id == following.Id);
            if (followRelation == null)
            {
                _logger.LogInformation("{FollowerUsername} is not following {FollowingUsername}", followerUsername, followingUsername);
                return false;
            }

            follower.FollowedUsers.Remove(followRelation);
            following.Followers.Remove(follower);

            await _context.SaveChangesAsync();

            _logger.LogInformation("{FollowerUsername} successfully unfollowed {FollowingUsername}", followerUsername, followingUsername);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UnfollowUserAsync. Follower: {FollowerUsername}, Following: {FollowingUsername}", followerUsername, followingUsername);
            return false;
        }
    }
}
