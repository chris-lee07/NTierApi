using BenefitsEstimator.Test;
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

        public ClientServiceTests()
        {
            _clientContext = new Mock<ClientContext>();
            _logger = new Mock<ILogger<ClientService>>();
            _clientService = new ClientService(_clientContext.Object, _logger.Object);
        }

        #region Variables
        private const int SomeClientId = 123;
        private const int SomeOtherClientId = 456;
        private const string SomeClientName = "SomeClientName";
        private const string SomeOtherClientName = "SomeOtherClientName";
        private const string SomeIndustry = "SomeIndustry";
        private const string SomeOtherIndustry = "SomeOtherIndustry";
        private const string SomeDescription = "SomeDescription";
        private const string SomeOtherDescription = "SomeOtherDescription";

        private ClientDbo SomeClientDbo;
        private ClientDbo SomeOtherClientDbo;
        #endregion

        [SetUp]
        public void Setup()
        {
            SomeClientDbo = new ClientDbo
            {
                ClientId = SomeClientId,
                ClientName = SomeClientName,
                Industry = SomeIndustry,
                Description = SomeDescription
            };
            SomeOtherClientDbo = new ClientDbo
            {
                ClientId = SomeOtherClientId,
                ClientName = SomeOtherClientName,
                Industry = SomeOtherIndustry,
                Description = SomeOtherDescription
            };

            var clientList = new List<ClientDbo> { SomeClientDbo, SomeOtherClientDbo };
            var clientDbSet = DbSetHelper.GetDbSetMock(clientList);
            _clientContext.Setup(ctx => ctx.Clients).Returns(clientDbSet.Object);
        }

        [Test]
        public async Task ReturnCorrectClient()
        {
            var client = await _clientService.GetByClientId(SomeClientId);

            Assert.That(client.ClientId, Is.EqualTo(SomeClientId));
        }
    }
}