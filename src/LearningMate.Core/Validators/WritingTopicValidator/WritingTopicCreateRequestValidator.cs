using FluentValidation;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Core.ErrorMessages;

namespace LearningMate.Core.Validators.WritingTopicValidator;

public class WritingTopicCreateRequestValidator : AbstractValidator<WritingTopicCreateRequestDto>
{
    public WritingTopicCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(nameof(WritingTopicCreateRequestDto.Content))
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(nameof(WritingTopicCreateRequestDto.Content))
            );
    }
}
