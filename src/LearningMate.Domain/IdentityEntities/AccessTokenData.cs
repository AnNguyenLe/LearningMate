namespace LearningMate.Domain.IdentityEntities;

public class AccessTokenData
{
    public required string AccessToken { get; set; }
    public DateTime ExpiresAt { get; set; }
}
