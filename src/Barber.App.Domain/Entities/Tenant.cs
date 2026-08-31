using System;

namespace Barber.App.Domain.Entities;
public class Tenant
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; } = default!;
    public string Slug { get; private set; } = default!;
    public string? Document { get; private set; }
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? TimeZone { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    protected Tenant() { }

    public Tenant(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }

    public void UpdateName(string name)
    {
        Name = name;
        UpdatedAt = DateTime.UtcNow;
    }
}
