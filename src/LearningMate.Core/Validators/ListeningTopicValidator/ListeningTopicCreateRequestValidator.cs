using FluentValidation;
using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Domain.Entities.Listening;

namespace LearningMate.Core.Validators.ListeningTopicValidator;

public class ListeningTopicCreateRequestValidator : AbstractValidator<ListeningTopicCreateRequestDto>
{
    public ListeningTopicCreateRequestValidator()
    {
        RuleFor(model => model.Content)
            .NotNull()
            .WithMessage(CommonErrorMessages.FieldCannotBeNull(nameof(ListeningTopic.Content)))
            .NotEmpty()
            .WithMessage(CommonErrorMessages.FieldCannotBeNull(nameof(ListeningTopic.Content)));
    }
}
