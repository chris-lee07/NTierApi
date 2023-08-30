using Microsoft.Extensions.Logging;
using NTierApi.Data.Models;
using NTierApi.Data.Repositories;

namespace NTierApi.Business
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;

        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }

        public async Task<ClientDbo> GetByClientId(int clientId)
        {
            return await _clientRepository.GetByClientId(clientId);
        }

        public async Task<ClientDbo> GetByClientByName(string clientName)
        {
            return await _clientRepository.GetByClientByName(clientName);
        }

        public async Task<IEnumerable<ClientDbo>> GetClients()
        {
            return await _clientRepository.GetClients();
        }
    }
}