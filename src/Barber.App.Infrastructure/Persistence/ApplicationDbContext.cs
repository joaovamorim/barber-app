using Microsoft.EntityFrameworkCore;
using Barber.App.Domain.Entities;
using Barber.App.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barber.App.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<Tenant> Tenants { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>(ConfigureTenant);

        // Example:
        // If you have entities with TenantId, you can apply a global query filter:
        // modelBuilder.Entity<SomeEntity>().HasQueryFilter(e => e.TenantId == _tenantProvider.TenantId);
    }

    private void ConfigureTenant(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Slug).HasMaxLength(100).IsRequired();
        builder.HasIndex(t => t.Slug).IsUnique();
        builder.Property(t => t.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(t => t.UpdatedAt).HasDefaultValueSql("now()");
    }
}
