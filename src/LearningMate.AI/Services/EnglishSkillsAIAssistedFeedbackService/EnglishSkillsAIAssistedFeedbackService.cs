using LearningMate.AI.ServiceContracts.EnglishSkillsAIAssistedFeedbackServiceContract;
using LearningMate.AI.ServiceContracts.PromptServiceContract;
using LearningMate.Core.ServiceContracts.ListeningTopicsServiceContract;
using LearningMate.Core.ServiceContracts.ReadingTopicsServiceContract;
using LearningMate.Core.ServiceContracts.SpeakingTopicsServiceContract;
using LearningMate.Core.ServiceContracts.WritingTopicsServiceContract;
using LearningMate.Speech.ServiceContracts;
using Microsoft.Extensions.Logging;

namespace LearningMate.AI.Services.EnglishSkillsAIAssistedFeedbackService;

// ---- ReadingTopicSubmitRequestDto ----
// {
//     id: "guid",
//     questions: [
//     {
//         id: "guid",
//         content: "How many countries are there in the world?",
//         answerOptions: [
//             {
//                 id: "guid",
//                 content: 195,
//                 isSelected: false
//             },
//             {
//                 id: "guid",
//                 content: 194,
//                 isSelected: true
//             },
//             {
//                 id: "guid",
//                 content: 193,
//                 isSelected: false
//             },
//             {
//                 id: "guid",
//                 content: 192,
//                 isSelected: false
//             }
//         ]
//     }
// ]
// }
// ----------------------------------------


// --- ReadingTopicSolutionResponseDto ---
// {
//     id: "guid",
//     category: "Geography",
//     title: "Countries of the world",
//     content: "Countries of the world Countries of the world Countries of the world Countries of the world",
//     questions: [
//          {
//              id: "guid",
//              content: "How many countries are there in the world?",
//              answerOptions: [
//                  {
//                      id: "guid",
//                      content: 195,
//                      IsCorrectAnswer: true
//                  },
//                  {
//                      id: "guid",
//                      content: 194,
//                      IsCorrectAnswer: false
//                  },
//                  {
//                      id: "guid",
//                      content: 193,
//                      IsCorrectAnswer: false
//                  },
//                  {
//                      id: "guid",
//                      content: 192,
//                      IsCorrectAnswer: false
//                  }
//               ]
//           }
//      ]
// }
// -------------------------------------------


public partial class EnglishSkillsAIAssistedFeedbackService(
    ILogger<EnglishSkillsAIAssistedFeedbackService> logger,
    IReadingTopicsService readingTopicsService,
    IListeningTopicsService listeningTopicsService,
    IWritingTopicsService writingTopicsService,
    ISpeakingTopicsService speakingTopicsService,
    IPromptService promptService,
    ISpeechService speechService
) : IEnglishSkillsAIAssistedFeedbackService
{
    private readonly ILogger<EnglishSkillsAIAssistedFeedbackService> _logger = logger;
    private readonly IReadingTopicsService _readingTopicsService = readingTopicsService;
    private readonly IListeningTopicsService _listeningTopicsService = listeningTopicsService;
    private readonly IWritingTopicsService _writingTopicsService = writingTopicsService;
    private readonly ISpeakingTopicsService _speakingTopicsService = speakingTopicsService;
    private readonly IPromptService _promptService = promptService;
    private readonly ISpeechService _speechService = speechService;
}
