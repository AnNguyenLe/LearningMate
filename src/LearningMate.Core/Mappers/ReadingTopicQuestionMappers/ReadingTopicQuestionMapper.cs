using System.Text.Json;
using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;
using LearningMate.Core.DTOs.ReadingTopicQuestionDTOs;
using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;
using LearningMate.Domain.Entities.Reading;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ReadingTopicQuestionMappers;

[Mapper]
public partial class ReadingTopicQuestionMapper
{
    [MapperIgnoreTarget(nameof(ReadingTopicQuestion.Id))]
    [MapperIgnoreTarget(nameof(ReadingTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreTarget(nameof(ReadingTopicQuestion.Topic))]
    private partial ReadingTopicQuestion ReadingTopicQuestionCreateRequestDtoToReadingTopicQuestion(
        ReadingTopicQuestionCreateRequestDto dto
    );

    [UserMapping(Default = false)]
    [MapperIgnoreTarget(nameof(MultipleChoiceAnswerOption.Id))]
    private partial MultipleChoiceAnswerOption MultipleChoiceAnswerOptionCreateRequestDtoToMultipleChoiceAnswerOption(
        MultipleChoiceAnswerOptionCreateRequestDto dto
    );

    public MultipleChoiceAnswerOption MapMultipleChoiceAnswerOptionCreateRequestDtoToMultipleChoiceAnswerOption(
        MultipleChoiceAnswerOptionCreateRequestDto dto
    )
    {
        var multipleChoiceAnswerOption =
            MultipleChoiceAnswerOptionCreateRequestDtoToMultipleChoiceAnswerOption(dto);
        multipleChoiceAnswerOption.Id = Guid.NewGuid();
        return multipleChoiceAnswerOption;
    }

    public ReadingTopicQuestion MapReadingTopicQuestionCreateRequestDtoToReadingTopicQuestion(
        ReadingTopicQuestionCreateRequestDto dto
    )
    {
        var topicQuestion = ReadingTopicQuestionCreateRequestDtoToReadingTopicQuestion(dto);
        topicQuestion.Id = Guid.NewGuid();
        topicQuestion.SerializedAnswerOptions = JsonSerializer.Serialize(
            topicQuestion.AnswerOptions
        );
        return topicQuestion;
    }

    [MapperIgnoreSource(nameof(ReadingTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreSource(nameof(ReadingTopicQuestion.Topic))]
    private partial ReadingTopicQuestionCreateResponseDto ReadingTopicQuestionToReadingTopicQuestionCreateResponseDto(
        ReadingTopicQuestion topicQuestion
    );

    public ReadingTopicQuestionCreateResponseDto MapReadingTopicQuestionToReadingTopicQuestionCreateResponseDto(
        ReadingTopicQuestion topicQuestion
    )
    {
        var dto = ReadingTopicQuestionToReadingTopicQuestionCreateResponseDto(topicQuestion);
        if (topicQuestion.SerializedAnswerOptions is null)
        {
            dto.AnswerOptions = [];
            return dto;
        }

        dto.AnswerOptions = JsonSerializer.Deserialize<List<MultipleChoiceAnswerOption>>(
            topicQuestion.SerializedAnswerOptions
        );
        return dto;
    }
}
