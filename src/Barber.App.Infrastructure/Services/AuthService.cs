using System;
using System.Threading.Tasks;
using Barber.App.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Barber.App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Barber.App.Domain.Entities;
using System.Linq;

namespace Barber.App.Infrastructure.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly Barber.App.Application.Interfaces.ITokenService _tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext dbContext, Barber.App.Application.Interfaces.ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _tokenService = tokenService;
    }

    public async Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) throw new Exception("Invalid credentials");
        var valid = await _userManager.CheckPasswordAsync(user, password);
        if (!valid) throw new Exception("Invalid credentials");

        var accessToken = _tokenService.GenerateJwtToken(user);
        var refreshTokenValue = Barber.App.Infrastructure.Authentication.RefreshTokenGenerator.GenerateToken();
        var refreshToken = new RefreshToken
        {
            UserId = Guid.Parse(user.Id.ToString()),
            Token = refreshTokenValue,
            ExpiresAt = Barber.App.Infrastructure.Authentication.RefreshTokenGenerator.GetExpiry(30),
            CreatedByIp = "127.0.0.1"
        };

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();

        return (accessToken, refreshTokenValue);
    }

    public async Task<(string accessToken, string refreshToken)> RefreshAsync(string token)
    {
        var existing = await _dbContext.RefreshTokens.Where(r => r.Token == token).OrderByDescending(r => r.CreatedAt).FirstOrDefaultAsync();
        if (existing == null || !existing.IsActive) throw new Exception("Invalid token");

        // revoke existing
        existing.RevokedAt = DateTime.UtcNow;
        existing.RevokedByIp = "127.0.0.1";

        // create new
        var newTokenValue = Barber.App.Infrastructure.Authentication.RefreshTokenGenerator.GenerateToken();
        var newRefresh = new RefreshToken
        {
            UserId = existing.UserId,
            Token = newTokenValue,
            ExpiresAt = Barber.App.Infrastructure.Authentication.RefreshTokenGenerator.GetExpiry(30),
            CreatedByIp = "127.0.0.1",
            ReplacedByToken = null
        };

        existing.ReplacedByToken = newTokenValue;
        _dbContext.RefreshTokens.Add(newRefresh);
        await _dbContext.SaveChangesAsync();

        var userId = existing.UserId;
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new Exception("User not found");

        var accessToken = _tokenService.GenerateJwtToken(user);
        return (accessToken, newTokenValue);
    }

    public async Task RevokeAsync(string token)
    {
        var existing = await _dbContext.RefreshTokens.Where(r => r.Token == token).OrderByDescending(r => r.CreatedAt).FirstOrDefaultAsync();
        if (existing == null) return;
        existing.RevokedAt = DateTime.UtcNow;
        existing.RevokedByIp = "127.0.0.1";
        await _dbContext.SaveChangesAsync();
    }
}
