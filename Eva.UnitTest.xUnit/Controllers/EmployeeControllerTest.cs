using Eva.Core.ApplicationService.Services;
using Eva.Core.Domain.BaseViewModels;
using Eva.EndPoint.API.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace Eva.UnitTest.xUnit.Controllers
{
    public class EmployeeControllerTest
    {
        [Fact]
        public void EmployeeGetAll()
        {
            // Arrange
            var employeeService = A.Fake<IEmployeeService>();
            var employeeController = new EmployeeController(employeeService);
            var getAllInput = BaseRequestViewModel.SampleBaseRequestViewModel();

            // Act
            var actionResult = employeeController.GetAllAsync(getAllInput);
            var okResult = new OkObjectResult(actionResult);

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
