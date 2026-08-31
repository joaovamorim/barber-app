using System;

namespace Barber.App.Infrastructure.MultiTenancy;
public interface ITenantProvider
{
    Guid? TenantId { get; }
    string? TenantSlug { get; }

    // Set the tenant for the current scope/request. Implementations may ignore in Noop.
    void SetTenant(Guid? id, string? slug);
}
