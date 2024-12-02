using FluentValidation;
using LearningMate.Core.DTOs.WritingTopicAnswerDTOs;
using LearningMate.Core.ErrorMessages;

namespace LearningMate.Core.Validators.WritingTopicAnswerValidator;

public class WritingTopicAnswerCreateRequestValidator
    : AbstractValidator<WritingTopicAnswerCreateRequestDto>
{
    public WritingTopicAnswerCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(
                    nameof(WritingTopicAnswerCreateRequestDto.Content)
                )
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(
                    nameof(WritingTopicAnswerCreateRequestDto.Content)
                )
            );
    }
}
