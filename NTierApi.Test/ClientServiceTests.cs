using AutoFixture;
using AutoFixture.AutoMoq;
using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NTierApi.Business;
using NTierApi.Data;
using NTierApi.Data.Models;

namespace NTierApi.Test
{
    public class ClientServiceTests
    {
        private readonly IClientService _clientService;
        private readonly Mock<ClientContext> _clientContext;
        private readonly Mock<ILogger<ClientService>> _logger;

        private ClientDbo SomeClientDbo;
        private ClientDbo SomeOtherClientDbo;
        private const int SomeClientId = 123;
        private const int SomeOtherClientId = 456;
        private const int SomeRandomClientId = 789;

        public ClientServiceTests()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            _clientContext = fixture.Freeze<Mock<ClientContext>>();
            _logger = fixture.Freeze<Mock<ILogger<ClientService>>>();
        }

        [SetUp]
        public void Setup()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            SomeClientDbo = fixture.Build<ClientDbo>().With(client => client.ClientId, SomeClientId).Create();
            SomeOtherClientDbo = fixture.Build<ClientDbo>().With(client => client.ClientId, SomeOtherClientId).Create();
        }

        [Test]
        public async Task ReturnCorrectClient()
        {
            // Arrange
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var logger = fixture.Freeze<Mock<ILogger<ClientService>>>();
            var clientContext = fixture.Freeze<Mock<ClientContext>>();
            var clientDbSet = fixture.Freeze<Mock<DbSet<ClientDbo>>>();
            clientDbSet.Setup(cds => cds.FirstOrDefaultAsync(c => c.ClientId == SomeClientId, It.IsAny<CancellationToken>())).ReturnsAsync(SomeClientDbo);
            clientContext.Setup(cc => cc.Clients).Returns(clientDbSet.Object);
            var clientService = fixture.Create<ClientService>();

            // Act
            var client = await clientService.GetByClientId(SomeClientId);

            // Assert
            Assert.That(client.ClientId, Is.EqualTo(SomeClientId));
        }
    }
}