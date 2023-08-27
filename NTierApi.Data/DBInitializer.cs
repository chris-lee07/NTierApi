using NTierApi.Data.Models;

namespace NTierApi.Data
{
    public static class DBInitializer
    {
        public static class DbInitializer
        {
            public static void Initialize(ClientContext context)
            {
                context.Database.EnsureCreated();

                SeedClients(context);
                SeedEmployees(context);
            }

            private static void SeedClients(ClientContext context)
            {
                // Check if already seeded
                if (context.Clients.Any())
                {
                    return;
                }

                var clients = new ClientDbo[]
                {
                    new ClientDbo { ClientName = "Subway", Description = "Fast-food sandwich shops", Industry = "Food" },
                    new ClientDbo { ClientName = "CVS", Description = "Pharmacy and casual goods store", Industry = "Retail" },
                    new ClientDbo { ClientName = "Pizza Hut", Description = "National pizza delivery chain", Industry = "Food" },
                    new ClientDbo { ClientName = "Pfizer", Description = "Research and production of pharmaceuticals", Industry = "Healthcare" },
                };

                foreach (ClientDbo c in clients)
                {
                    context.Clients.Add(c);
                }
                context.SaveChanges();
            }

            private static void SeedEmployees(ClientContext context)
            {
                // Check if already seeded
                if (context.Employees.Any())
                {
                    return;
                }

                var employees = new EmployeeDbo[]
                {
                    new EmployeeDbo { First = "John", Last = "Smith", Position = "Sandwich Artist", ClientId = 1 },
                    new EmployeeDbo { First = "Carrie", Last = "Fisher", Position = "Sandwich Artist", ClientId = 1 },
                    new EmployeeDbo { First = "Danielle", Last = "Martinez", Position = "Team Manager", ClientId = 1 },
                    new EmployeeDbo { First = "Jane", Last = "Lim", Position = "Stocker", ClientId = 2 },
                    new EmployeeDbo { First = "Ernest", Last = "Hemingway", Position = "CEO", ClientId = 2 },
                    new EmployeeDbo { First = "Ashu", Last = "Sharma", Position = "Delivery Driver", ClientId = 3 },
                    new EmployeeDbo { First = "Bojan", Last = "Bogdanovic", Position = "Line Chef", ClientId = 3 },
                    new EmployeeDbo { First = "Henrietta", Last = "Miles", Position = "Shift Manager", ClientId = 3 },
                    new EmployeeDbo { First = "Megan", Last = "Markle", Position = "CEO", ClientId = 4 },
                    new EmployeeDbo { First = "Harry", Last = "Prince", Position = "CFO", ClientId = 4 },
                    new EmployeeDbo { First = "William", Last = "Prince", Position = "CTO", ClientId = 4 }
                };

                foreach (EmployeeDbo e in employees)
                {
                    context.Employees.Add(e);
                }
                context.SaveChanges();
            }
        }
    }
}
