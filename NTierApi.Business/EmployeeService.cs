using Microsoft.Extensions.Logging;
using NTierApi.Data.Models;
using NTierApi.Data.Repositories;

namespace NTierApi.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<EmployeeDbo> GetEmployee(int employeeId, int clientId)
        {
            return await _employeeRepository.GetEmployee(employeeId, clientId);
        }

        public async Task<IEnumerable<EmployeeDbo>> GetEmployees(int cliendId)
        {
            return await _employeeRepository.GetEmployees(cliendId);
        }
    }
}
