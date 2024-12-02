using LearningMate.Core.DTOs.WritingTopicAnswerDTOs;
using LearningMate.Domain.Entities.Writing;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.WritingTopicAnswerMappers;

[Mapper]
public partial class WritingTopicAnswerMapper
{
    [MapperIgnoreTarget(nameof(WritingTopicAnswer.Id))]
    [MapperIgnoreTarget(nameof(WritingTopicAnswer.Topic))]
    public partial WritingTopicAnswer WritingTopicAnswerCreateRequestDtoToWritingTopicAnswer(
        WritingTopicAnswerCreateRequestDto dto
    );

    public WritingTopicAnswer MapWritingTopicAnswerCreateRequestDtoToWritingTopicAnswer(
        WritingTopicAnswerCreateRequestDto dto
    )
    {
        var topic = WritingTopicAnswerCreateRequestDtoToWritingTopicAnswer(dto);
        topic.Id = Guid.NewGuid();
        return topic;
    }

    [MapperIgnoreSource(nameof(WritingTopicAnswer.Topic))]
    public partial WritingTopicAnswerCreateResponseDto MapWritingTopicAnswerToWritingTopicAnswerCreateResponseDto(
        WritingTopicAnswer topicAnswer
    );
}
