using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTierApi.Data.Models;

namespace NTierApi.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ClientContext _dbContext;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(ClientContext dbContext, ILogger<ClientRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ClientDbo> GetByClientId(int clientId)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<ClientDbo> GetByClientByName(string clientName)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.ClientName == clientName);
        }

        public async Task<IEnumerable<ClientDbo>> GetClients()
        {
            return await _dbContext.Clients.ToListAsync();

            //return await _dbContext.Clients.FromSqlRaw("SELECT ClientId, ClientName, Industry, Description FROM dbo.Clients").ToListAsync<ClientDbo>();
        }
    }
}
