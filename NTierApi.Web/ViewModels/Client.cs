using NTierApi.Data.Models;

namespace NTierApi.Web.ViewModels
{
    public class Client
    {
        public Client() { }
        public Client(ClientDbo client)
        {
            ClientId = client.ClientId;
            ClientName = client.ClientName;
            Industry = client.Industry;
            Description = client.Description;
        }

        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
    }
}
