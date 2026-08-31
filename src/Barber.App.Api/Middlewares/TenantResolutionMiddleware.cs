using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Barber.App.Infrastructure.Persistence;
using Barber.App.Infrastructure.MultiTenancy;
using Barber.App.Domain.Entities;
using Serilog;

namespace Barber.App.Api.Middlewares;

public class TenantResolutionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger = Log.ForContext<TenantResolutionMiddleware>();

    public TenantResolutionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext, ITenantProvider tenantProvider)
    {
        try
        {
            string? tenantSlug = null;

            // 1) try header X-Tenant-Slug
            if (context.Request.Headers.TryGetValue("X-Tenant-Slug", out var headerValues) && headerValues.Count > 0)
            {
                tenantSlug = headerValues[0];
            }

            // 2) try subdomain resolution (tenant.example.com)
            if (string.IsNullOrEmpty(tenantSlug) && context.Request.Host.HasValue)
            {
                var host = context.Request.Host.Host; // e.g., tenant.example.com or localhost
                // simple heuristic: if host contains more than two segments and not localhost/127.0.0.1
                if (!host.Equals("localhost", StringComparison.OrdinalIgnoreCase) && !host.Equals("127.0.0.1") )
                {
                    var parts = host.Split('.', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 3)
                    {
                        tenantSlug = parts[0];
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(tenantSlug))
            {
                // lookup tenant by slug (case-insensitive)
                var tenant = await dbContext.Tenants
                    .AsNoTracking()
                    .Where(t => t.Slug.ToLower() == tenantSlug.ToLower())
                    .FirstOrDefaultAsync();

                if (tenant != null)
                {
                    // set tenant in provider for this scope
                    tenantProvider.SetTenant(tenant.Id, tenant.Slug);
                    _logger.Information("Tenant resolved from request. TenantSlug={TenantSlug}, TenantId={TenantId}", tenant.Slug, tenant.Id);
                }
                else
                {
                    _logger.Warning("Tenant slug not found: {TenantSlug}", tenantSlug);
                }
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error while resolving tenant");
            throw;
        }
    }
}
