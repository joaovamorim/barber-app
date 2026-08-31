using System;
using System.Security.Cryptography;
using System.Text;

namespace Barber.App.Infrastructure.Authentication;

public static class TokenHasher
{
    public static string HashToken(string token)
    {
        if (string.IsNullOrEmpty(token)) return string.Empty;
        using var sha = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(token);
        var hash = sha.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
