using Microsoft.EntityFrameworkCore;
using NTierApi.Business;
using NTierApi.Data;
using static NTierApi.Data.DBInitializer;

// Initialize configuration
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
IConfigurationRoot configuration = configurationBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// In comparison to leveraging AutoFac for Dependency Injection (DI), this use's Microsoft's native Dependency Injection
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering DB Context against SQL Server database
builder.Services.AddDbContext<ClientContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Register interfaces to services
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ClientContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
