using LearningMate.Domain.IdentityEntities;

namespace LearningMate.Core.ServiceContracts.Authentication;

public interface IJwtService
{
    AccessTokenData GenerateAccessToken(AppUser user, List<string> roles);
}
