using System;

namespace Barber.App.Infrastructure.MultiTenancy;
public interface ITenantProvider
{
    Guid? TenantId { get; }
    string? TenantSlug { get; }
}
