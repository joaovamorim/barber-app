using System.Threading.Tasks;
using Barber.App.Infrastructure.Persistence;
using Barber.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Barber.App.Infrastructure.Identity;
using System;

namespace Barber.App.Infrastructure.Seeding;

public static class DbSeeder
{
    private static readonly ILogger _logger = Log.ForContext(typeof(DbSeeder));

    public static async Task SeedAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
    {
        _logger.Information("Applying migrations and seeding demo data (if missing)");

        // Apply migrations
        await context.Database.MigrateAsync();

        if (!await context.Tenants.AnyAsync())
        {
            _logger.Information("Seeding demo tenant: Barbearia Demo");
            var tenant = new Tenant("Barbearia Demo", "barbearia-demo");
            context.Tenants.Add(tenant);
            await context.SaveChangesAsync();

            // create roles and an owner user
            var roleManager = serviceProvider.GetService(typeof(RoleManager<IdentityRole<Guid>>)) as RoleManager<IdentityRole<Guid>>;
            var userManager = serviceProvider.GetService(typeof(UserManager<ApplicationUser>)) as UserManager<ApplicationUser>;
            if (roleManager != null && userManager != null)
            {
                var roles = new[] { "Owner", "Admin", "Manager", "Receptionist", "Professional" };
                foreach (var r in roles)
                {
                    if (!await roleManager.RoleExistsAsync(r))
                        await roleManager.CreateAsync(new IdentityRole<Guid>(r));
                }

                var ownerEmail = "owner@barbearia.demo";
                if (await userManager.FindByEmailAsync(ownerEmail) == null)
                {
                    var owner = new ApplicationUser
                    {
                        UserName = ownerEmail,
                        Email = ownerEmail,
                        DisplayName = "Owner Demo",
                        TenantId = tenant.Id
                    };
                    var result = await userManager.CreateAsync(owner, "DevPass123!");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(owner, "Owner");
                    }
                }
            }
        }
        else
        {
            _logger.Information("Tenants already exist; skipping tenant seed");
        }
    }
}
