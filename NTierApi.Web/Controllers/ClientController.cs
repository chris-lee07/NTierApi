using Microsoft.AspNetCore.Mvc;
using NTierApi.Business;
using NTierApi.Web.ViewModels;

namespace NTierApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService clientService, ILogger<ClientController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Client>> GetClients()
        {
            var results = await _clientService.GetClients();

            return results.Select(client => new Client(client));
        }

        [HttpGet("{id}")]
        public async Task<Client> GetClient(int id)
        {
            var result = await _clientService.GetByClientId(id);

            if (result == null)
            {
                var error = "No client by the provided clientId exists";
                _logger.Log(LogLevel.Error, error);

                return null;
            }

            return new Client(result);
        }

        [HttpGet("{name}")]
        public async Task<Client> GetClient(string name)
        {
            var result = await _clientService.GetByClientByName(name);

            if (result == null)
            {
                var error = "No client by the provided client name exists";
                _logger.Log(LogLevel.Error, error);

                return null;
            }

            return new Client(result);
        }
    }
}
