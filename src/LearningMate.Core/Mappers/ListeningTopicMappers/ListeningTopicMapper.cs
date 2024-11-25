using LearningMate.Core.DTOs.ListeningTopicDTOs;
using LearningMate.Domain.Entities.Listening;
using Riok.Mapperly.Abstractions;

namespace LearningMate.Core.Mappers.ListeningTopicMappers;

[Mapper]
public partial class ListeningTopicMapper
{
    [MapperIgnoreTarget(nameof(ListeningTopic.Id))]
    [MapperIgnoreTarget(nameof(ListeningTopic.Exam))]
    [MapperIgnoreTarget(nameof(ListeningTopic.Score))]
    [MapperIgnoreTarget(nameof(ListeningTopic.Questions))]
    public partial ListeningTopic ListeningTopicCreateRequestDtoToListeningTopic(
        ListeningTopicCreateRequestDto dto
    );

    public ListeningTopic MapListeningTopicCreateRequestDtoToListeningTopic(
        ListeningTopicCreateRequestDto dto
    )
    {
        var topic = ListeningTopicCreateRequestDtoToListeningTopic(dto);
        topic.Id = Guid.NewGuid();
        topic.Score = 0;
        return topic;
    }

    [MapperIgnoreSource(nameof(ListeningTopic.Score))]
    [MapperIgnoreSource(nameof(ListeningTopic.Exam))]
    [MapperIgnoreSource(nameof(ListeningTopic.Questions))]
    public partial ListeningTopicCreateResponseDto MapListeningTopicToListeningTopicCreateResponseDto(
        ListeningTopic dto
    );
}
