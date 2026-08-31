namespace Barber.App.Application.Interfaces;
using Barber.App.Infrastructure.Identity;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<(string accessToken, string refreshToken)> LoginAsync(string email, string password);
    Task<(string accessToken, string refreshToken)> RefreshAsync(string token);
    Task RevokeAsync(string token);
}
