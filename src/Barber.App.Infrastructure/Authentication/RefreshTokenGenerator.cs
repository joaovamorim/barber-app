using System;
using System.Security.Cryptography;

namespace Barber.App.Infrastructure.Authentication;

public static class RefreshTokenGenerator
{
    public static string GenerateToken(int size = 64)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static DateTime GetExpiry(int days = 30)
    {
        return DateTime.UtcNow.AddDays(days);
    }
}
