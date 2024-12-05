using FluentResults;
using LearningMate.Core.DTOs.ReadingTopicDTOs;

namespace LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;

public interface IReadingTopicsRetrieveService
{
    Task<Result<ReadingTopicSolutionResponseDto>> GetTopicSolutionAsync(
        Guid id
    );
}
