namespace LearningMate.Core.ErrorMessages;

public static class CommonErrorMessages
{
    public const string UnableToIdentifyUser = "Unable to identify user.";
    public const string InvalidIdFormat = "Invalid ID format.";

    public static string FailedTo(string action) => $"Failed to {action}.";

    public const string FailedToCreate = "Failed to create {entityName}.";

    public static string FieldCannotBeNull(string fieldName) => $"{fieldName} cannot be null.";

    public static string FieldCannotBeEmpty(string fieldName) => $"{fieldName} cannot be empty.";

    public const string MakeSureAllRequiredFieldsAreProperlyEnter =
        "Make sure all required fields are properly entered.";

    public static string UnexpectedErrorHappenedDuringProcess(string processName) =>
        $"Unexpected error happened during {processName}. Please contact the support team.";
}
