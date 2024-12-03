using LearningMate.Core.DTOs.SpeakingTopicAnswerDTOs;
using LearningMate.Domain.Entities.Speaking;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.SpeakingTopicAnswerMappers;

[Mapper]
public partial class SpeakingTopicAnswerMapper
{
    [MapperIgnoreTarget(nameof(SpeakingTopicAnswer.Id))]
    [MapperIgnoreTarget(nameof(SpeakingTopicAnswer.Topic))]
    public partial SpeakingTopicAnswer SpeakingTopicAnswerCreateRequestDtoToSpeakingTopicAnswer(
        SpeakingTopicAnswerCreateRequestDto dto
    );
    public SpeakingTopicAnswer MapSpeakingTopicAnswerCreateRequestDtoToSpeakingTopicAnswer(
        SpeakingTopicAnswerCreateRequestDto dto
    )
    {
        var topic = SpeakingTopicAnswerCreateRequestDtoToSpeakingTopicAnswer(dto);
        topic.Id = Guid.NewGuid();
        return topic;
    }
    [MapperIgnoreSource(nameof(SpeakingTopicAnswer.Topic))]
    public partial SpeakingTopicAnswerCreateResponseDto MapSpeakingTopicAnswerToSpeakingTopicAnswerCreateResponseDto(
        SpeakingTopicAnswer topicAnswer
    );
}
