using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTierApi.Data.Models
{
    [PrimaryKey(nameof(ClientId))]
    public class ClientDbo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Industry { get; set; }
        public string Description { get; set; }
        
        public ICollection<EmployeeDbo> Employees { get; set; }
    }
}
