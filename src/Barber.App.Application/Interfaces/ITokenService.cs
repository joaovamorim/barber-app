namespace Barber.App.Application.Interfaces;
public interface ITokenService
{
    string GenerateJwtToken(object user);
}
