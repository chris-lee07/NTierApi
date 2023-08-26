using NTierApi.Data.Models;

namespace NTierApi.Business
{
    public interface IClientService
    {
        Task<ClientDbo> GetByClientId(int clientId);

        Task<ClientDbo> GetByClientByName(string clientName);

        Task<IEnumerable<ClientDbo>> GetClients();
    }
}
