using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.Extensions.Logging;
using Moq;
using NTierApi.Business;
using NTierApi.Data;
using NTierApi.Data.Models;
using NTierApi.Data.Repositories;

namespace NTierApi.Test
{
    public class ClientServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IClientRepository> _clientRepo;
        private readonly Mock<ILogger<ClientService>> _logger;

        private ClientDbo SomeClientDbo;
        private const int SomeClientId = 123;
        private const int SomeOtherClientId = 456;
        private const string SomeClientName = "SomeClientName";
        private const string SomeOtherClientName = "SomeOtherClientName";

        public ClientServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization() { ConfigureMembers = true });
            _fixture.Inject<ClientDbo>(null);
            _logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            _clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
        }

        [SetUp]
        public void Setup()
        {
            SomeClientDbo = _fixture.Build<ClientDbo>().With(client => client.ClientId, SomeClientId).With(client => client.ClientName, SomeClientName).Create();
        }

        [Test]
        public async Task ReturnCorrectClientByClientId()
        {
            // Arrange
            var logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
            clientRepo.Setup(cr => cr.GetByClientId(SomeClientId)).ReturnsAsync(SomeClientDbo);
            var clientService = _fixture.Create<ClientService>();

            // Act
            var client = await clientService.GetByClientId(SomeClientId);

            // Assert
            Assert.That(client.ClientId, Is.EqualTo(SomeClientId));
            Assert.That(client.ClientName, Is.EqualTo(SomeClientName));
        }

        [Test]
        public async Task ReturnsNoClientByClientId()
        {
            // Arrange
            var logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
            clientRepo.Setup(cr => cr.GetByClientId(SomeClientId)).ReturnsAsync(SomeClientDbo);
            var clientService = _fixture.Create<ClientService>();

            // Act
            var client = await clientService.GetByClientId(SomeOtherClientId);

            // Assert
            Assert.IsNull(client);
        }

        [Test]
        public async Task ReturnCorrectClientByClientName()
        {
            // Arrange
            var logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
            clientRepo.Setup(cr => cr.GetByClientByName(SomeClientName)).ReturnsAsync(SomeClientDbo);
            var clientService = _fixture.Create<ClientService>();

            // Act
            var client = await clientService.GetByClientByName(SomeClientName);

            // Assert
            Assert.That(client.ClientId, Is.EqualTo(SomeClientId));
            Assert.That(client.ClientName, Is.EqualTo(SomeClientName));
        }

        [Test]
        public async Task ReturnsNoClientByClientName()
        {
            // Arrange
            var logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
            clientRepo.Setup(cr => cr.GetByClientByName(SomeClientName)).ReturnsAsync(SomeClientDbo);
            var clientService = _fixture.Create<ClientService>();

            // Act
            var client = await clientService.GetByClientByName(SomeOtherClientName);

            // Assert
            Assert.IsNull(client);
        }

        [Test]
        public async Task ReturnsAllClients()
        {
            // Arrange
            int clientCount = 5;
            var logger = _fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientRepo = _fixture.Freeze<Mock<IClientRepository>>();
            var clients = _fixture.Build<ClientDbo>().CreateMany(clientCount).ToList();
            clientRepo.Setup(cr => cr.GetClients()).ReturnsAsync(clients);
            var clientService = _fixture.Create<ClientService>();

            // Act
            var results = await clientService.GetClients();

            // Assert
            Assert.That(results.Count(), Is.EqualTo(clientCount));
        }
    }
}