using LearningMate.Core.DTOs.ReadingTopicDTOs;
using LearningMate.Domain.Entities.Reading;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ReadingTopicMappers;

[Mapper]
public partial class ReadingTopicMapper
{
    [MapperIgnoreTarget(nameof(ReadingTopic.Id))]
    [MapperIgnoreTarget(nameof(ReadingTopic.Exam))]
    [MapperIgnoreTarget(nameof(ReadingTopic.Score))]
    [MapperIgnoreTarget(nameof(ReadingTopic.Questions))]
    public partial ReadingTopic ReadingTopicCreateRequestDtoToReadingTopic(
        ReadingTopicCreateRequestDto dto
    );

    public ReadingTopic MapReadingTopicCreateRequestDtoToReadingTopic(
        ReadingTopicCreateRequestDto dto
    )
    {
        var topic = ReadingTopicCreateRequestDtoToReadingTopic(dto);
        topic.Id = Guid.NewGuid();
        topic.Score = 0;
        return topic;
    }

    [MapperIgnoreSource(nameof(ReadingTopic.Score))]
    [MapperIgnoreSource(nameof(ReadingTopic.Exam))]
    [MapperIgnoreSource(nameof(ReadingTopic.Questions))]
    public partial ReadingTopicCreateResponseDto MapReadingTopicToReadingTopicCreateResponseDto(
        ReadingTopic dto
    );
}
