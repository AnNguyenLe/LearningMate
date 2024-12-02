using FluentValidation;
using LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Domain.Entities.Reading;

namespace LearningMate.Core.Validators.ReadingTopicQuestionValidator;

public class ReadingTopicQuestionCreateRequestValidator
    : AbstractValidator<ReadingTopicQuestionCreateRequestDto>
{
    public ReadingTopicQuestionCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(nameof(ReadingTopicQuestion.Content))
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(nameof(ReadingTopicQuestion.Content))
            );
    }
}
