using NTierApi.Data.Models;

namespace NTierApi.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDbo> GetEmployee(int employeeId, int clientId);

        Task<IEnumerable<EmployeeDbo>> GetEmployees(int cliendId);
    }
}
