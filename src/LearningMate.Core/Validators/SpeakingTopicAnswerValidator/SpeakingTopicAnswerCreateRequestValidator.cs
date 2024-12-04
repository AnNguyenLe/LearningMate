using FluentValidation;
using LearningMate.Core.DTOs.SpeakingTopicAnswerDTOs;
using LearningMate.Core.ErrorMessages;

namespace LearningMate.Core.Validators.SpeakingTopicAnswerValidator;

public class SpeakingTopicAnswerCreateRequestValidator
    : AbstractValidator<SpeakingTopicAnswerCreateRequestDto>
{
    public SpeakingTopicAnswerCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(
                    nameof(SpeakingTopicAnswerCreateRequestDto.Content)
                )
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(
                    nameof(SpeakingTopicAnswerCreateRequestDto.Content)
                )
            );
    }
}
