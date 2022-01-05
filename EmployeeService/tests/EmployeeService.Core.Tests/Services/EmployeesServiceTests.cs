using Xunit;
using Moq;
using AutoFixture;
using FluentAssertions;
using EmployeeService.Core.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using EmployeeService.Core.Services;
using System.Threading.Tasks;
using EmployeeService.Core.Models;
using System.Collections.Generic;
using System;

namespace EmployeeService.Core.Tests
{
    public class EmployeesServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly Mock<ILogger<EmployeesService>> _loggerMock;
        private readonly EmployeesService _sut;

        public EmployeesServiceTests()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IEmployeeRepository>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger<EmployeesService>>>();
            _sut = new EmployeesService(_repositoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task GetAllEmployees_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var employeesMock = _fixture.Create<IEnumerable<Employee>>();
            _repositoryMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employeesMock);

            // Act
            var result = await _sut.GetAllEmployees().ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IEnumerable<Employee>>();
            _repositoryMock.Verify(x => x.GetAllEmployees(), Times.Once());
        }

        [Fact]
        public async Task GetAllEmployees_ShouldReturnNull_WhenDataNotFound()
        {
            // Arrange
            IEnumerable<Employee> employeesMock = null;
            _repositoryMock.Setup(x => x.GetAllEmployees()).ReturnsAsync(employeesMock);

            // Act
            var result = await _sut.GetAllEmployees().ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(x => x.GetAllEmployees(), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnData_WhenDataFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var employeeMock = _fixture.Create<Employee>();
            _repositoryMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(employeeMock);

            // Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Employee>();
            _repositoryMock.Verify(x => x.GetEmployeeById(id), Times.Once());
        }

        [Fact]
        public async Task GetEmployeeById_ShouldReturnNull_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            Employee employee = null;
            _repositoryMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(employee);

            // Act
            var result = await _sut.GetEmployeeById(id).ConfigureAwait(false);

            // Assert
            result.Should().BeNull();
            _repositoryMock.Verify(x => x.GetEmployeeById(id), Times.Once());
        }

        [Fact]
        public void GetEmployeeById_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            int id = 0;
            Employee employee = null;
            _repositoryMock.Setup(x => x.GetEmployeeById(id)).ReturnsAsync(employee);

            // Act
            Func<Employee> result = () => _sut.GetEmployeeById(id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateEmployee_ShouldReturnData_WhenDataIsInsertedSuccessfully()
        {
            // Arrange
            var employeeMock = _fixture.Create<Employee>();
            _repositoryMock.Setup(x => x.CreateEmployee(employeeMock)).ReturnsAsync(employeeMock);

            // Act
            var result = await _sut.CreateEmployee(employeeMock).ConfigureAwait(false);


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Employee>();
            _repositoryMock.Verify(x => x.CreateEmployee(employeeMock), Times.Once());
        }

        [Fact]
        public void CreateEmployee_ShouldThrowNullReferenceException_WhenInputIsNull()
        {
            // Arrange
            var employeeMock = _fixture.Create<Employee>();
            Employee request = null;
            _repositoryMock.Setup(x => x.CreateEmployee(employeeMock)).ReturnsAsync(employeeMock);

            // Act
            Func<Employee> result = () => _sut.CreateEmployee(request).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnTrue_WhenDataIsDeletedSuccessfully()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _repositoryMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(true);

            // Act
            var result = await _sut.DeleteEmployee(id).ConfigureAwait(false);


            // Assert
            result.Should().BeTrue();
            _repositoryMock.Verify(x => x.DeleteEmployee(id), Times.Once());
        }

        [Fact]
        public async Task DeleteEmployee_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            _repositoryMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(false);

            // Act
            var result = await _sut.DeleteEmployee(id).ConfigureAwait(false);


            // Assert
            result.Should().BeFalse();
            _repositoryMock.Verify(x => x.DeleteEmployee(id), Times.Once());
        }

        [Fact]
        public void DeleteEmployee_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            var response = _fixture.Create<bool>();
            var id = 0;
            _repositoryMock.Setup(x => x.DeleteEmployee(id)).ReturnsAsync(response);

            // Act
            Func<bool> result = () => _sut.DeleteEmployee(id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnTrue_WhenDataUpdatedSuccessfully()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var employeeMock = _fixture.Create<Employee>();
            _repositoryMock.Setup(x => x.UpdateEmployee(employeeMock, id)).ReturnsAsync("Update success!");

            // Act
            var result = await _sut.UpdateEmployee(employeeMock, id).ConfigureAwait(false);


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<string>();
            _repositoryMock.Verify(x => x.UpdateEmployee(employeeMock, id), Times.Once());
        }

        [Fact]
        public async Task UpdateEmployee_ShouldReturnFalse_WhenDataNotFound()
        {
            // Arrange
            var id = _fixture.Create<int>();
            var employeeMock = _fixture.Create<Employee>();
            _repositoryMock.Setup(x => x.UpdateEmployee(employeeMock, id)).ReturnsAsync("Not found!");

            // Act
            var result = await _sut.UpdateEmployee(employeeMock, id).ConfigureAwait(false);


            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<string>();
            _repositoryMock.Verify(x => x.UpdateEmployee(employeeMock, id), Times.Once());
        }

        [Fact]
        public async Task UpdateEmployee_ShouldThrowNullReferenceException_WhenInputIsEqualsZero()
        {
            // Arrange
            int id = 0;
            var employeeMock = _fixture.Create<Employee>();
            _repositoryMock.Setup(x => x.UpdateEmployee(employeeMock, id)).ReturnsAsync("Not found!");

            // Act
            Func<Object> result = () => _sut.UpdateEmployee(employeeMock, id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateEmployee_ShouldThrowNullReferenceException_WhenRequestIsNull()
        {
            // Arrange
            int id = 0;
            var employeeMock = _fixture.Create<Employee>();
            Employee request = null;
            _repositoryMock.Setup(x => x.UpdateEmployee(employeeMock, id)).ReturnsAsync("Not found!");

            // Act
            Func<Object> result = () => _sut.UpdateEmployee(request, id).Result;

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }
    }
}