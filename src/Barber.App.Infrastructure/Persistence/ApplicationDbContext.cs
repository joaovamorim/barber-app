using Microsoft.EntityFrameworkCore;
using Barber.App.Domain.Entities;
using Barber.App.Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace Barber.App.Infrastructure.Persistence;
public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    private readonly ITenantProvider _tenantProvider;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<Tenant> Tenants { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tenant>(ConfigureTenant);
        modelBuilder.Entity<RefreshToken>(ConfigureRefreshToken);

        // Apply global query filter for entities implementing IHasTenant
        var hasTenantInterface = typeof(Barber.App.Domain.IHasTenant);
        var entityTypes = modelBuilder.Model.GetEntityTypes().Where(t => t.ClrType != null && hasTenantInterface.IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            var method = typeof(ApplicationDbContext).GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Instance)!.MakeGenericMethod(entityType.ClrType);
            method.Invoke(this, new object[] { modelBuilder });
        }
    }

    private void SetGlobalQueryFilter<TEntity>(ModelBuilder builder) where TEntity : class, Barber.App.Domain.IHasTenant
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(typeof(TEntity), "e");
        var tenantProperty = System.Linq.Expressions.Expression.Property(parameter, "TenantId"); // Guid

        var providerExpr = System.Linq.Expressions.Expression.Constant(_tenantProvider);
        var providerTenantIdProperty = System.Linq.Expressions.Expression.Property(providerExpr, "TenantId"); // Guid?

        var providerIsNull = System.Linq.Expressions.Expression.Equal(providerTenantIdProperty, System.Linq.Expressions.Expression.Constant(null, typeof(Guid?)));

        var convertProviderTenant = System.Linq.Expressions.Expression.Convert(providerTenantIdProperty, typeof(Guid));
        var tenantEquals = System.Linq.Expressions.Expression.Equal(tenantProperty, convertProviderTenant);

        var body = System.Linq.Expressions.Expression.OrElse(providerIsNull, tenantEquals);

        var lambda = System.Linq.Expressions.Expression.Lambda(body, parameter);

        var entity = builder.Entity<TEntity>();
        entity.HasQueryFilter((System.Linq.Expressions.LambdaExpression)lambda);
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

    private void ConfigureRefreshToken(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Token).IsRequired();
        builder.Property(r => r.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(r => r.ExpiresAt).IsRequired();
        builder.HasIndex(r => r.UserId);
    }
}
