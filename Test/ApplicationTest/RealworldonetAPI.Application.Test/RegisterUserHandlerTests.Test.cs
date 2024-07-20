using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using RealworldonetAPI.Application.Commands.User;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.DTO.user.RealworldonetAPI.Domain.DTO.user;
using RealworldonetAPI.Domain.Entities;

namespace RealworldonetAPI.Tests.Handlers.User
{
    public class RegisterUserHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly RegisterUserHandler _handler;

        public RegisterUserHandlerTests()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            _tokenServiceMock = new Mock<ITokenService>();

            _handler = new RegisterUserHandler(_userManagerMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public async Task Handle_SuccessfulRegistration_ReturnsUserDtoWithToken()
        {
            // Arrange
            var command = new RegisterUserCommand
            {
                userdto = new UserRegisterWrapper
                {
                    User = new UserRegisterDto
                    {
                        Username = "newUser",
                        Email = "new@example.com",
                        Password = "Password1"
                    }
                }
            };
            _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _tokenServiceMock.Setup(x => x.CreateToken(It.IsAny<ApplicationUser>())).ReturnsAsync("GeneratedToken");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Token.Should().Be("GeneratedToken");
            result.UserName.Should().Be(command.userdto.User.Username);
            result.Email.Should().Be(command.userdto.User.Email);
            result.Image.Should().Be("https://api.realworld.io/images/smiley-cyrus.jpeg");
        }

        



    }
}
