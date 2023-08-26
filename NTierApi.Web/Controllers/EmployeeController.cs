using Microsoft.AspNetCore.Mvc;
using NTierApi.Business;
using NTierApi.Web.ViewModels;

namespace NTierApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService EmployeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = EmployeeService;
            _logger = logger;
        }

        [HttpGet("clientId/{id}")]
        public async Task<IEnumerable<Employee>> GetEmployees(int id)
        {
            var results = await _employeeService.GetEmployees(id);

            return results.Select(Employee => new Employee(Employee));
        }

        [HttpGet("{clientId}/{employeeId}")]
        public async Task<Employee> GetEmployee(int clientId, int employeeId)
        {
            var result = await _employeeService.GetEmployee(employeeId, clientId);

            if (result == null)
            {
                var error = "No Employee by the provided employeeId and client Id exists";
                _logger.Log(LogLevel.Error, error);

                return null;
            }

            return new Employee(result);
        }
    }
}
