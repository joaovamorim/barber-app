using System;

namespace Barber.App.Infrastructure.MultiTenancy;
public class TenantProvider : ITenantProvider
{
    private Guid? _tenantId;
    private string? _tenantSlug;

    public Guid? TenantId => _tenantId;
    public string? TenantSlug => _tenantSlug;

    public void SetTenant(Guid? id, string? slug)
    {
        _tenantId = id;
        _tenantSlug = slug;
    }
}
