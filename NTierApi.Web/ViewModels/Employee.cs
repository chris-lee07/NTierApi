using NTierApi.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierApi.Web.ViewModels
{
    public class Employee
    {
        public Employee()
        {

        }

        public Employee(EmployeeDbo employee)
        {
            ClientId = employee.ClientId;
            EmployeeId = employee.EmployeeId;
            FirstName = employee.First;
            LastName = employee.Last;
            Position = employee.Position;
        }

        public int ClientId { get; set; }
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
    }
}
