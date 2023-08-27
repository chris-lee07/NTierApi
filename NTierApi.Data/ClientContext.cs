using Microsoft.EntityFrameworkCore;
using NTierApi.Data.Models;

namespace NTierApi.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {

        }

        public ClientContext() { }

        public virtual DbSet<ClientDbo> Clients { get; set; }
        public virtual DbSet<EmployeeDbo> Employees { get; set; }
    }
}