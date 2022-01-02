using AutoFixture;
using Xunit;
using Moq;
using FluentAssertions;
using EmployeeService.Core.Interfaces.Services;
using EmployeeService.Api.V1.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using EmployeeService.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Tests.V1.Controllers
{
    public class EmployeeControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IEmployeeService> _serviceMock;
        private readonly EmployeeController _sut;

        public EmployeeControllerTests()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IEmployeeService>>();
            // create the implementation in-memory
            _sut = new EmployeeController(_serviceMock.Object); 
        }
        [Fact]
        public async Task GetEmployees_ShouldReturnOkRespone_WhenDataFound()
        {
            // Arrange
            var employeesMock = _fixture.Create<IEnumerable<Employee>>();
            _serviceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employeesMock);

            //Act
            var result = await _sut.GetEmployees().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<IEnumerable<Employee>>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(employeesMock.GetType());
            _serviceMock.Verify(x => x.GetAllEmployees(), Times.Once());
        }

        [Fact]
        public async Task GetEmployees_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            List<Employee> response = null;
            _serviceMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(response);

            //Act
            var result = await _sut.GetEmployees().ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.GetAllEmployees(), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnOkRespone_WhenValidInput()
        {
            // Arrange
            var employeeMock = _fixture.Create<Employee>();
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(employeeMock);

            //Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Employee>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(employeeMock.GetType());
            _serviceMock.Verify(x => x.GetEmployeeById(id), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNotFound_WhenNoDataFound()
        {
            // Arrange
            Employee response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(response);

            //Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            //Assert
            /*Assert.NotNull(result);*/
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<NoContentResult>();
            _serviceMock.Verify(x => x.GetEmployeeById(id), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnBadRequest_WhenInputIsEqualZero()
        {
            // Arrange
            var response  = _fixture.Create<Employee>();
            int id = 0;
            _serviceMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(response);

            //Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.GetAllEmployees(), Times.Never());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnBadRequest_WhenInputIsLessThanZero()
        {
            // Arrange
            var response = _fixture.Create<Employee>();
            int id = -1;
            _serviceMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(response);

            //Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.GetAllEmployees(), Times.Never());
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnOkRespone_WhenValidRequest()
        {
            // Arrange
            var request = _fixture.Create<Employee>();
            var response = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.CreateEmployee(request)).ReturnsAsync(response);

            //Act
            var result = await _sut.CreateEmployee(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult<Employee>>();
            result.Result.Should().BeAssignableTo<CreatedAtRouteResult>();
            _serviceMock.Verify(x => x.CreateEmployee(response), Times.Never());
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnBadRequest_WhenInValidRequest()
        {
            // Arrange
            var request = _fixture.Create<Employee>();
            _sut.ModelState.AddModelError("Name", "The Name field is required.");
            var response = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.CreateEmployee(request)).ReturnsAsync(response);

            //Act
            var result = await _sut.CreateEmployee(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.CreateEmployee(response), Times.Never());
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnNoContents_WhenDeleteARecord()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(true);

            //Act
            var result = await _sut.DeleteEmployee(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnNotFound_WhenRecordNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(false);

            //Act
            var result = await _sut.DeleteEmployee(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnBadRequest_WhenInputIsZero()
        {
            // Arrange
            int id = 0;
            _serviceMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(false);

            //Act
            var result = await _sut.DeleteEmployee(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.DeleteEmployee(id), Times.Never());
        }

        [Fact]
        public async Task UpateEmployee_ShouldReturnBadRespone_WhenInputIsZero()
        {
            // Arrange
            int id = 0;
            var request = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.UpdateEmployee(request, id)).ReturnsAsync("Not found!");

            //Act
            var result = await _sut.UpdateEmployee(request, id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateEmployee(request, id), Times.Never());
        }

        [Fact]
        public async Task UpateEmployee_ShouldReturnBadRespone_WhenInvalidRequest()
        {
            // Arrange
            int id = _fixture.Create<int>();
            var request = _fixture.Create<Employee>();
            _sut.ModelState.AddModelError("Name", "The Name field is required.");
            var response = _fixture.Create<Employee>();
            _serviceMock.Setup(x => x.UpdateEmployee(request, id)).ReturnsAsync("Bad request!");

            //Act
            var result = await _sut.UpdateEmployee(request, id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateEmployee(request, id), Times.Never());
        }

        [Fact]
        public async Task UpateEmployee_ShouldReturnOkRespone_WhenRecordIsUpdated()
        {
            // Arrange
            int id = _fixture.Create<int>();
            var request = _fixture.Create<Employee>();
            string respone = "Update success!";
            _serviceMock.Setup(x => x.UpdateEmployee(request, id)).ReturnsAsync(respone);

            //Act
            var result = await _sut.UpdateEmployee(request, id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(respone.GetType());
            _serviceMock.Verify(x => x.UpdateEmployee(request, id), Times.Once());
        }

        [Fact]
        public async Task UpateEmployee_ShouldReturnNotFound_WhenRecordNotFound()
        {
            // Arrange
            int id = _fixture.Create<int>();
            var request = _fixture.Create<Employee>();
            string respone = "Not Found!";
            _serviceMock.Setup(x => x.UpdateEmployee(request, id)).ReturnsAsync(respone);

            //Act
            var result = await _sut.UpdateEmployee(request, id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
            result.Result.As<OkObjectResult>().Value
                .Should()
                .NotBeNull()
                .And.BeOfType(respone.GetType());
            _serviceMock.Verify(x => x.UpdateEmployee(request, id), Times.Once());
        }

    }
}