using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagementSystem.Domain.Configurations;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Service;

namespace TMS.Service.Test
{
    public class JwtServiceTest
    {
        [Fact]
        public void GenerateJwtToken_ShouldReturnValidToken_WhenUserIsProvided()
        {
            // Arrange
            var options = Options.Create(new JwtSettings
            {
                Key = "secretkey123456789012345678901234",
                Issuer = "testIssuer",
                Audience = "testAudience",
                Expires = TimeSpan.FromHours(1)
            });
            var jwtService = new JwtService(options);
            var user = new User { Id = Guid.NewGuid(), UserName = "validUser", Email = "user@example.com" };

            // Act
            var token = jwtService.GenerateJwtToken(user);

            // Assert
            Assert.NotNull(token);
            Assert.Contains("eyJ", token); // JWT starts with "eyJ"

            // Try to decode the token and validate claims
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

            Assert.NotNull(decodedToken);

            // Check the Issuer and Audience from the Payload
            Assert.Equal(options.Value.Issuer, decodedToken?.Payload[JwtRegisteredClaimNames.Iss]);
            Assert.Equal(options.Value.Audience, decodedToken?.Payload[JwtRegisteredClaimNames.Aud]);

            // Check claims
            Assert.Contains(decodedToken.Claims, c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == user.Id.ToString());
            Assert.Contains(decodedToken.Claims, c => c.Type == JwtRegisteredClaimNames.Email && c.Value == user.Email);
            Assert.Contains(decodedToken.Claims, c => c.Type == ClaimTypes.Name && c.Value == user.UserName);
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnTokenWithExpiryTime_WhenExpiresIsSet()
        {
            // Arrange
            var options = Options.Create(new JwtSettings
            {
                Key = "secretkey123456789012345678901234",
                Issuer = "testIssuer",
                Audience = "testAudience",
                Expires = TimeSpan.FromHours(1)
            });
            var jwtService = new JwtService(options);
            var user = new User { Id = Guid.NewGuid(), UserName = "validUser", Email = "user@example.com" };

            // Act
            var token = jwtService.GenerateJwtToken(user);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            var expiry = decodedToken?.ValidTo;

            Assert.NotNull(expiry);
            Assert.True(expiry > DateTime.UtcNow);
        }
    }
}
