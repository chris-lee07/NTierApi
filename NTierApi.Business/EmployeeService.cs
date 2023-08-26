using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTierApi.Data;
using NTierApi.Data.Models;

namespace NTierApi.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ClientContext _dbContext;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ClientContext dbContext, ILogger<EmployeeService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<EmployeeDbo> GetEmployee(int employeeId, int clientId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(e => e.ClientId == clientId && e.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<EmployeeDbo>> GetEmployees(int cliendId)
        {
            return await _dbContext.Employees.Where(e => e.ClientId == cliendId).ToListAsync();
        }
    }
}
