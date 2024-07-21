using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RealworldonetAPI.Application.Commands.User;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Tests.Handlers.User
{
    public class UserLoginHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<ILoggerManager> _loggerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly UserLogin.UserLoginHandler _handler;

        public UserLoginHandlerTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object,
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<ApplicationUser>>(),
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                Mock.Of<IServiceProvider>(),
                Mock.Of<ILogger<UserManager<ApplicationUser>>>()
            );

            _loggerMock = new Mock<ILoggerManager>();
            _tokenServiceMock = new Mock<ITokenService>();

            _handler = new UserLogin.UserLoginHandler(_userManagerMock.Object, _loggerMock.Object, _tokenServiceMock.Object);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(default(ApplicationUser));

        }


        // Test for UserLoginHandler
        [Fact]
        public async Task Handle_GivenValidCredentials_ShouldReturnUserWithToken()
        {
            // Arrange
            var user = new ApplicationUser
            {
                Email = "test@example.com",
                UserName = "testUser",
                Bio = "Test Bio",
                Image = "Test Image"
            };
            var token = "testToken";
            var request = new UserLogin
            {
                loginUserDto = new LoginUserWrapper
                {
                    User = new LoginUser
                    {
                        Email = user.Email,
                        Password = "TestPassword"
                    }
                }
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(user.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, request.loginUserDto.User.Password)).ReturnsAsync(true);
            _tokenServiceMock.Setup(x => x.CreateToken(user)).ReturnsAsync(token);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(new
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
            });
        }

        // Test for UserLoginHandler

        [Fact]
        public async Task Handle_GivenInvalidCredentials_ShouldReturnError()
        {
            // Arrange
            var request = new UserLogin
            {
                loginUserDto = new LoginUserWrapper
                {
                    User = new LoginUser
                    {
                        Email = "wrong@example.com",
                        Password = "WrongPassword"
                    }
                }
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(new { errors = new { body = new[] { "Email or Password is incorrect" } } });
        }
    }
}
