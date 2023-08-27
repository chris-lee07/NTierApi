using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierApi.Data.Models
{
    [PrimaryKey(nameof(EmployeeId))]
    public class EmployeeDbo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Position { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public ClientDbo Client { get; set; }
    }
}
