using FluentValidation;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Core.Validators.SharedValidators;
using LearningMate.Domain.Entities.Listening;

namespace LearningMate.Core.Validators.ListeningTopicQuestionValidator;

public class ListeningTopicQuestionCreateRequestValidator
    : AbstractValidator<ListeningTopicQuestionCreateRequestDto>
{
    public ListeningTopicQuestionCreateRequestValidator()
    {
        RuleFor(x => x.Content)
            .NotNull()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeNull(nameof(ListeningTopicQuestion.Content))
            )
            .NotEmpty()
            .WithMessage(
                CommonErrorMessages.FieldCannotBeEmpty(nameof(ListeningTopicQuestion.Content))
            );
    }
}
