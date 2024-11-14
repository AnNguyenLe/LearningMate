using System.Text.Json;
using FluentValidation;

namespace LearningMate.Core.Validators.SharedValidators;

public static class ParsableJsonValidator
{
    public static IRuleBuilderOptions<T, string?> MustBeParsableJson<T>(
        this IRuleBuilder<T, string?> ruleBuilder
    )
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .Must(
                (obj, json, context) =>
                {
                    if (string.IsNullOrWhiteSpace(json))
                        return false;
                    try
                    {
                        // Attempt to deserialize the string to a generic object
                        var parsedObject = JsonSerializer.Deserialize<object>(json);
                        return parsedObject != null;
                    }
                    catch (JsonException)
                    {
                        return false;
                    }
                }
            )
            .WithMessage("The provided string is not a valid JSON.");
    }
}
