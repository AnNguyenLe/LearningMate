using FluentValidation;
using LearningMate.Core.DTOs.ExamDTOs;
using LearningMate.Core.ErrorMessages;
using LearningMate.Domain.Entities;

namespace LearningMate.Core.Validators.ExamValidator;

public class ExamCreateRequestValidator : AbstractValidator<ExamCreateRequestDto>
{
    public ExamCreateRequestValidator()
    {
        RuleFor(model => model.Title)
            .NotNull()
            .WithMessage(CommonErrorMessages.FieldCannotBeNull(nameof(Exam.Title)))
            .NotEmpty()
            .WithMessage(CommonErrorMessages.FieldCannotBeEmpty(nameof(Exam.Title)));
    }
}
