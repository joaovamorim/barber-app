using System;

namespace Barber.App.Domain;

public interface IHasTenant
{
    Guid TenantId { get; }
}
