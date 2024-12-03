using System.Text.Json;
using LearningMate.Core.DTOs.SpeakingTopicDTOs;
using LearningMate.Domain.Entities.Speaking;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.SpeakingTopicMappers;

[Mapper]
public partial class SpeakingTopicMapper
{
    [MapperIgnoreTarget(nameof(SpeakingTopic.Id))]
    [MapperIgnoreTarget(nameof(SpeakingTopic.Exam))]
    [MapperIgnoreTarget(nameof(SpeakingTopic.Score))]
    [MapperIgnoreTarget(nameof(SpeakingTopic.SerializedResourcesUrl))]
    [MapperIgnoreTarget(nameof(SpeakingTopic.Answers))]
    public partial SpeakingTopic SpeakingTopicCreateRequestDtoToSpeakingTopic(
        SpeakingTopicCreateRequestDto dto
    );
    public SpeakingTopic MapSpeakingTopicCreateRequestDtoToSpeakingTopic(
        SpeakingTopicCreateRequestDto dto
    )
    {
        var topic = SpeakingTopicCreateRequestDtoToSpeakingTopic(dto);
        topic.Id = Guid.NewGuid();
        topic.Score = 0;
        topic.SerializedResourcesUrl = JsonSerializer.Serialize(topic.ResourcesUrl ?? []);
        return topic;
    }
    [MapperIgnoreSource(nameof(SpeakingTopic.Score))]
    [MapperIgnoreSource(nameof(SpeakingTopic.Exam))]
    [MapperIgnoreSource(nameof(SpeakingTopic.SerializedResourcesUrl))]
    [MapperIgnoreSource(nameof(SpeakingTopic.Answers))]
    public partial SpeakingTopicCreateResponseDto MapSpeakingTopicToSpeakingTopicCreateResponseDto(
        SpeakingTopic topic
    );
}
