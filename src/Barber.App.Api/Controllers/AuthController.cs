using System.Threading.Tasks;
using Barber.App.Infrastructure.Identity;
using Barber.App.Infrastructure.Authentication;
using Barber.App.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Barber.App.Infrastructure.MultiTenancy;

namespace Barber.App.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly ITenantProvider _tenantProvider;

    public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService, ITenantProvider tenantProvider)
    {
        _userManager = userManager;
        _tokenService = tokenService;
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
        // Require tenant context for registration (owner creates user within a tenant)
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

        // For now, do not assign roles (will be handled by admin later)
        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { token });
    }

    public class LoginRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return Unauthorized(new { error = "Invalid credentials" });

        var valid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!valid)
            return Unauthorized(new { error = "Invalid credentials" });

        var token = _tokenService.GenerateJwtToken(user);
        return Ok(new { token });
    }
}
