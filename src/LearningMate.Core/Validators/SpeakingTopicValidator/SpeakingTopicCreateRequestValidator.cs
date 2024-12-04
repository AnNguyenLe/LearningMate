using FluentValidation;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Core.ErrorMessages;

namespace LearningMate.Core.Validators.SpeakingTopicValidator;

public class SpeakingTopicCreateRequestValidator
    : AbstractValidator<SpeakingTopicCreateRequestDto>
{
    public SpeakingTopicCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(nameof(SpeakingTopicCreateRequestDto.Content))
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(nameof(SpeakingTopicCreateRequestDto.Content))
            );
    }
}
