using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.Core.Domain.Models;
using Eva.EndPoint.API.Controllers;
using FakeItEasy;
using FluentAssertions;

namespace Eva.UnitTest.xUnit.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        public UserControllerTest()
        {
            _userService = A.Fake<IUserService>();
        }

        [Fact]
        public void UserController_AlterAdminStateAsync_ReturnsTaskOfObject()
        {
            // Arrange
            int userId = 1;
            var output = A.Fake<Task<ActionResultViewModel<User>>>();
            A.CallTo(() => _userService.AlterAdminStateAsync(userId)).Returns(output);
            var controller = new UserController(_userService);

            // Act
            var result = controller.AlterAdminStateAsync(userId);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
