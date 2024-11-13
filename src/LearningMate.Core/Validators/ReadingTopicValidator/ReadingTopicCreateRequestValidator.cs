using FluentValidation;
using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Domain.Entities.Reading;

namespace LearningMate.Core.Validators.ReadingTopicValidator;

public class ReadingTopicCreateRequestValidator : AbstractValidator<ReadingTopicCreateRequestDto>
{
    public ReadingTopicCreateRequestValidator()
    {
        RuleFor(model => model.Content)
            .NotNull()
            .WithMessage(CommonErrorMessages.FieldCannotBeNull(nameof(ReadingTopic.Content)))
            .NotEmpty()
            .WithMessage(CommonErrorMessages.FieldCannotBeNull(nameof(ReadingTopic.Content)));
    }
}
