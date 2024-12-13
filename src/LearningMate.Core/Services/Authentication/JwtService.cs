using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearningMate.Core.Common.Authorization;
using LearningMate.Core.ConfigurationOptions.Jwt;
using LearningMate.Core.ServiceContracts.Authentication;
using LearningMate.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LearningMate.Core.Services.Authentication;

public class JwtService(
    ILogger<JwtService> logger,
    IOptions<JwtConfiguration> jwtConfiguration,
    UserManager<AppUser> userManager
) : IJwtService
{
    private readonly ILogger<JwtService> _logger = logger;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly JwtConfiguration _jwtConfiguration = jwtConfiguration.Value;

    public AccessTokenData GenerateAccessToken(AppUser user, List<string> roles)
    {
        _logger.LogInformation(nameof(GenerateAccessToken));

        var expiresAt = DateTime.UtcNow.AddHours(_jwtConfiguration.AccessTokenLifeTimeInHours);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new(JwtRegisteredClaimNames.Exp, expiresAt.ToString()),
            new(ClaimTypes.NameIdentifier, user.Email!),
            new(ClaimTypes.Name, user.Email!),
            new(ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
        ];
        if (roles.Count > 0)
        {
            roles.ForEach(role =>
            {
                claims.Add(new(ClaimTypes.Role, role));
            });
        }

        return new AccessTokenData
        {
            AccessToken = GenerateJwtToken(claims, expiresAt),
            ExpiresAt = expiresAt
        };
    }

    private string GenerateJwtToken(IEnumerable<Claim> claims, DateTime expiresAt)
    {
        _logger.LogInformation(nameof(GenerateJwtToken));

        var key = Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        var signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256Signature
        );

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            NotBefore = DateTime.UtcNow,
            Expires = expiresAt,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
