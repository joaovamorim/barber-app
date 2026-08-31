using System.Threading.Tasks;
using Barber.App.Infrastructure.Services;
using Barber.App.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Barber.App.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Barber.App.Infrastructure.MultiTenancy;

namespace Barber.App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITenantProvider _tenantProvider;

    public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager, ITenantProvider tenantProvider)
    {
        _authService = authService;
        _userManager = userManager;
        _tenantProvider = tenantProvider;
    }

    public class RegisterRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? DisplayName { get; set; }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest model)
    {
        var tenantId = _tenantProvider.TenantId;
        if (tenantId == null)
            return BadRequest(new { error = "Tenant not resolved. Include X-Tenant-Slug header or use tenant subdomain." });

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            DisplayName = model.DisplayName,
            TenantId = tenantId
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        var (accessToken, refreshToken) = await _authService.LoginAsync(model.Email, model.Password);
        return Ok(new { accessToken, refreshToken });
    }

    public class LoginRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        try
        {
            var (accessToken, refreshToken) = await _authService.LoginAsync(model.Email, model.Password);
            return Ok(new { accessToken, refreshToken });
        }
        catch
        {
            return Unauthorized(new { error = "Invalid credentials" });
        }
    }

    public class RefreshRequest { public string RefreshToken { get; set; } = default!; }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest model)
    {
        try
        {
            var (accessToken, refreshToken) = await _authService.RefreshAsync(model.RefreshToken);
            return Ok(new { accessToken, refreshToken });
        }
        catch
        {
            return Unauthorized(new { error = "Invalid refresh token" });
        }
    }

    public class RevokeRequest { public string RefreshToken { get; set; } = default!; }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeRequest model)
    {
        await _authService.RevokeAsync(model.RefreshToken);
        return Ok(new { revoked = true });
    }
}
