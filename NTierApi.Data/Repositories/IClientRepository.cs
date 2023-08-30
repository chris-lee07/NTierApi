using NTierApi.Data.Models;

namespace NTierApi.Data.Repositories
{
    public interface IClientRepository
    {
        Task<ClientDbo> GetByClientId(int clientId);

        Task<ClientDbo> GetByClientByName(string clientName);

        Task<IEnumerable<ClientDbo>> GetClients();
    }
}
