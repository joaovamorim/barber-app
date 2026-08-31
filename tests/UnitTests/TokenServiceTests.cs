using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Barber.App.Infrastructure.Authentication;
using Barber.App.Infrastructure.Identity;

namespace UnitTests
{
    public class TokenServiceTests
    {
        [Fact]
        public void GenerateJwtToken_ReturnsToken()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"JWT__Secret", "dev_secret_for_tests_1234567890"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var tokenService = new TokenService(configuration);
            var user = new ApplicationUser { Id = Guid.NewGuid(), Email = "test@example.com", DisplayName = "Test" };
            var token = tokenService.GenerateJwtToken(user);
            Assert.False(string.IsNullOrWhiteSpace(token));
        }
    }
}
