namespace LearningMate.Core.DTOs.Authentication;

public class AuthenticationResponse
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string AccessToken { get; set; }
    public DateTime ExpiryOfAccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
