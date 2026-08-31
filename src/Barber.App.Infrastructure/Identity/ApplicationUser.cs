using System;
using Microsoft.AspNetCore.Identity;

namespace Barber.App.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public Guid? TenantId { get; set; }
    public string? DisplayName { get; set; }
}
