using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using Microsoft.Extensions.Logging;

namespace LearningMate.Core.Services.ListeningTopicsService;

public partial class ListeningTopicsService(ILogger<ListeningTopicsService> logger)
    : IListeningTopicsService
{
    private readonly ILogger<ListeningTopicsService> _logger = logger;
}
