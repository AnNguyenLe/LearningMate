using FluentResults;
using Microsoft.AspNetCore.Http;

namespace LearningMate.Speech.ServiceContracts;

public interface ISpeechService
{
    Task<Result<string>> TranscribeAudioAsync(IFormFile audioFile);
}
