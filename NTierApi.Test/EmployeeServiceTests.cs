using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using Moq;
using NTierApi.Business;
using NTierApi.Data.Models;
using NTierApi.Data.Repositories;

namespace NTierApi.Test
{
    public class EmployeeServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IEmployeeRepository> _employeeRepo;
        private readonly Mock<ILogger<EmployeeService>> _logger;

        private EmployeeDbo SomeEmployeeDbo;
        private const int SomeClientId = 123;
        private const int SomeOtherClientId = 456;
        private const int SomeEmployeeId = 777;
        private const int SomeOtherEmployeeId = 888;

        public EmployeeServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
            _fixture.Inject<EmployeeDbo>(null);
            _logger = _fixture.Freeze<Mock<ILogger<EmployeeService>>>();
            _employeeRepo = _fixture.Freeze<Mock<IEmployeeRepository>>();
        }

        [SetUp]
        public void Setup()
        {
            SomeEmployeeDbo = _fixture.Build<EmployeeDbo>().With(emp => emp.ClientId, SomeClientId).With(emp => emp.EmployeeId, SomeEmployeeId).Create();
        }

        [Test]
        public async Task ReturnCorrectEmployee()
        {
            // Arrange
            _employeeRepo.Setup(er => er.GetEmployee(SomeEmployeeId, SomeClientId)).ReturnsAsync(SomeEmployeeDbo);
            var employeeService = _fixture.Create<EmployeeService>();

            // Act
            var result = await employeeService.GetEmployee(SomeEmployeeId, SomeClientId);

            // Assert
            Assert.That(result.ClientId, Is.EqualTo(SomeClientId));
            Assert.That(result.EmployeeId, Is.EqualTo(SomeEmployeeId));
        }

        [Test]
        [TestCase(SomeOtherEmployeeId, SomeClientId)]
        [TestCase(SomeEmployeeId, SomeOtherClientId)]
        [TestCase(SomeOtherEmployeeId, SomeOtherClientId)]
        public async Task ReturnsNoEmployee(int employeeId, int clientId)
        {
            // Arrange
            _employeeRepo.Setup(er => er.GetEmployee(SomeEmployeeId, SomeClientId)).ReturnsAsync(SomeEmployeeDbo);
            var employeeService = _fixture.Create<EmployeeService>();

            // Act
            var result = await employeeService.GetEmployee(employeeId, clientId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ReturnsAllClientEmployees()
        {
            // Arrange
            int employeeCount = 5;
            var employees = _fixture.Build<EmployeeDbo>().CreateMany(employeeCount).ToList();
            _employeeRepo.Setup(cr => cr.GetEmployees(SomeClientId)).ReturnsAsync(employees);
            var employeeService = _fixture.Create<EmployeeService>();

            // Act
            var results = await employeeService.GetEmployees(SomeClientId);

            // Assert
            Assert.That(results.Count(), Is.EqualTo(employeeCount));
        }

        [Test]
        public async Task ReturnsNoClientEmployees()
        {
            // Arrange
            int employeeCount = 5;
            var employees = _fixture.Build<EmployeeDbo>().CreateMany(employeeCount).ToList();
            _employeeRepo.Setup(cr => cr.GetEmployees(SomeClientId)).ReturnsAsync(employees);
            var employeeService = _fixture.Create<EmployeeService>();

            // Act
            var results = await employeeService.GetEmployees(SomeOtherClientId);

            // Assert
            Assert.That(results.Count(), Is.EqualTo(0));
        }
    }
}
