using System;

namespace Barber.App.Infrastructure.MultiTenancy;
public class NoopTenantProvider : ITenantProvider
{
    // Placeholder implementation for Fase 1.
    // Fase 2: implementar resolução via subdomínio, token ou header.
    public Guid? TenantId => null;
    public string? TenantSlug => null;
}
