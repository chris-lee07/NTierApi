using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NTierApi.Data.Models;

namespace NTierApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ClientContext _dbContext;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(ClientContext dbContext, ILogger<EmployeeRepository> logger)
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
