namespace LearningMate.Core.ConfigurationOptions.Jwt;

public class JwtConfiguration
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public required string SecretKey { get; set; }
    public int AccessTokenLifeTimeInHours { get; set; }
    public int RefreshTokenLifeTimeInSeconds { get; set; }
}
