using LearningMate.Core.Mappers.ExamMappers;
using LearningMate.Core.ServiceContracts.ExamsServiceContract;
using LearningMate.Domain.RepositoryContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ExamsService;

public partial class ExamsService(
    ILogger<ExamsService> logger,
    IExamsRepository examsRepository,
    ExamMapper examMapper
) : IExamsService
{
    private readonly ILogger<ExamsService> _logger = logger;
    private readonly IExamsRepository _examsRepository = examsRepository;
    private readonly ExamMapper _examMapper = examMapper;
}
