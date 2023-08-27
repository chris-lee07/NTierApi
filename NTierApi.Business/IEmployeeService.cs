using Microsoft.EntityFrameworkCore;
using NTierApi.Data.Models;

namespace NTierApi.Business
{
    public interface IEmployeeService
    {
        Task<EmployeeDbo> GetEmployee(int employeeId, int clientId);

        Task<IEnumerable<EmployeeDbo>> GetEmployees(int cliendId);
    }
}
