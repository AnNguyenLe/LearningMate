using System.Text.Json;
using LearningMate.Core.DTOs.MultipleChoiceAnswerOptionDTOs;
using LearningMate.Core.DTOs.ListeningTopicQuestionDTOs;
using LearningMate.Domain.Entities.QuestionTypes.MultipleChoice;
using LearningMate.Domain.Entities.Listening;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ListeningTopicQuestionMappers;

[Mapper]
public partial class ListeningTopicQuestionMapper {
    [MapperIgnoreTarget(nameof(ListeningTopicQuestion.Id))]
    [MapperIgnoreTarget(nameof(ListeningTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreTarget(nameof(ListeningTopicQuestion.Topic))]
    private partial ListeningTopicQuestion ListeningTopicQuestionCreateRequestDtoToListeningTopicQuestion(
        ListeningTopicQuestionCreateRequestDto dto
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

    public ListeningTopicQuestion MapListeningTopicQuestionCreateRequestDtoToListeningTopicQuestion(
        ListeningTopicQuestionCreateRequestDto dto
    )
    {
        var topicQuestion = ListeningTopicQuestionCreateRequestDtoToListeningTopicQuestion(dto);
        topicQuestion.Id = Guid.NewGuid();
        topicQuestion.SerializedAnswerOptions = JsonSerializer.Serialize(
            topicQuestion.AnswerOptions
        );
        return topicQuestion;
    }

    [MapperIgnoreSource(nameof(ListeningTopicQuestion.SerializedAnswerOptions))]
    [MapperIgnoreSource(nameof(ListeningTopicQuestion.Topic))]
    private partial ListeningTopicQuestionCreateResponseDto ListeningTopicQuestionToListeningTopicQuestionCreateResponseDto(
        ListeningTopicQuestion topicQuestion
    );

    public ListeningTopicQuestionCreateResponseDto MapListeningTopicQuestionToListeningTopicQuestionCreateResponseDto(
        ListeningTopicQuestion topicQuestion
    )
    {
        var dto = ListeningTopicQuestionToListeningTopicQuestionCreateResponseDto(topicQuestion);
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
