using FluentValidation;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.ErrorMessages;

namespace LearningMate.Core.Validators.SharedValidators;

public static class GuidValidator
{
    public static IRuleBuilderOptions<T, string?> MustBeValidGuid<T>(
        this IRuleBuilder<T, string?> ruleBuilder
    )
    {
        return ruleBuilder
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(nameof(ReadingTopicCreateRequestDto.ExamId))
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(nameof(ReadingTopicCreateRequestDto.ExamId))
            )
            .NotEmpty()
            .Must(guid => Guid.TryParse(guid, out _))
            .WithMessage(CommonErrorMessages.InvalidIdFormat);
    }
}
