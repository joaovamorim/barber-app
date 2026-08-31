using System.Threading.Tasks;
using Barber.App.Infrastructure.Persistence;
using Barber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Barber.App.Infrastructure.Seeding;

public static class DbSeeder
{
    private static readonly ILogger _logger = Log.ForContext(typeof(DbSeeder));

    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // For development convenience we'll use EnsureCreated to build the schema from the model.
        // In production, prefer using explicit EF Migrations and avoid EnsureCreated.
        _logger.Information("Ensuring database is created and applying seed data (if missing)");
        context.Database.EnsureCreated();

        if (!await context.Tenants.AnyAsync())
        {
            _logger.Information("Seeding demo tenant: Barbearia Demo");
            var tenant = new Tenant("Barbearia Demo", "barbearia-demo");
            context.Tenants.Add(tenant);
            await context.SaveChangesAsync();
        }
        else
        {
            _logger.Information("Tenants already exist; skipping tenant seed");
        }
    }
}
