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
        _logger.Information("Applying migrations and seeding demo data (if missing)");

        // Apply migrations (preferred) - in development it's OK
        await context.Database.MigrateAsync();

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
