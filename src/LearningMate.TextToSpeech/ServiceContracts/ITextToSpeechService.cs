using FluentResults;

namespace LearningMate.TextToSpeech.ServiceContracts;

public interface ITextToSpeechService
{
    Task<Result<MemoryStream>> SynthesizeAsync(string sampleText);
}
