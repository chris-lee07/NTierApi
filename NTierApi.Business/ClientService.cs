using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTierApi.Data;
using NTierApi.Data.Models;

namespace NTierApi.Business
{
    public class ClientService : IClientService
    {
        private readonly ClientContext _dbContext;
        private readonly ILogger<ClientService> _logger;

        public ClientService(ClientContext dbContext, ILogger<ClientService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ClientDbo> GetByClientId(int clientId)
        {
            return await _dbContext.Clients.Include(c => c.Employees).FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<ClientDbo> GetByClientByName(string clientName)
        {
            return await _dbContext.Clients.Include(c => c.Employees).FirstOrDefaultAsync(c => c.ClientName == clientName);
        }

        public async Task<IEnumerable<ClientDbo>> GetClients()
        {
            return await _dbContext.Clients.Include(c => c.Employees).ToListAsync();
        }
    }
}