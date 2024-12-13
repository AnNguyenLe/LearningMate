namespace LearningMate.Core.Common.Authorization;

public static class AppUserRoles
{
    public const string ADMIN = "Admin";

    public static bool HasRole(this IList<string> userRoles, string expectingRole)
    {
        return userRoles.Any(role => role == expectingRole);
    }
}
