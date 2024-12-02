using System.Text.Json;
using LearningMate.Core.DTOs.WritingTopicDTOs;
using LearningMate.Domain.Entities.Writing;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.WritingTopicMappers;

[Mapper]
public partial class WritingTopicMapper
{
    [MapperIgnoreTarget(nameof(WritingTopic.Id))]
    [MapperIgnoreTarget(nameof(WritingTopic.Exam))]
    [MapperIgnoreTarget(nameof(WritingTopic.Score))]
    [MapperIgnoreTarget(nameof(WritingTopic.SerializedResourcesUrl))]
    [MapperIgnoreTarget(nameof(WritingTopic.Answers))]
    public partial WritingTopic WritingTopicCreateRequestDtoToWritingTopic(
        WritingTopicCreateRequestDto dto
    );

    public WritingTopic MapWritingTopicCreateRequestDtoToWritingTopic(
        WritingTopicCreateRequestDto dto
    )
    {
        var topic = WritingTopicCreateRequestDtoToWritingTopic(dto);
        topic.Id = Guid.NewGuid();
        topic.Score = 0;
        topic.SerializedResourcesUrl = JsonSerializer.Serialize(topic.ResourcesUrl ?? []);
        return topic;
    }

    [MapperIgnoreSource(nameof(WritingTopic.Score))]
    [MapperIgnoreSource(nameof(WritingTopic.Exam))]
    [MapperIgnoreSource(nameof(WritingTopic.SerializedResourcesUrl))]
    [MapperIgnoreSource(nameof(WritingTopic.Answers))]
    public partial WritingTopicCreateResponseDto MapWritingTopicToWritingTopicCreateResponseDto(
        WritingTopic topic
    );
}
