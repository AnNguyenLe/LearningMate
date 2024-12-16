using FluentResults;
using Google.Cloud.Speech.V1;
using LearningMate.Core.ConfigurationOptions.GCP;
using LearningMate.Speech.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LearningMate.Speech.Services.GCP;

public class GCPSpeechService(IOptions<GoogleCredentialOptions> googleCredentialOptions)
    : ISpeechService
{
    private readonly GoogleCredentialOptions _googleCredentialOptions =
        googleCredentialOptions.Value;

    public async Task<Result<string>> TranscribeAudioAsync(IFormFile audioFile)
    {
        using var memoryStream = new MemoryStream();
        await audioFile.CopyToAsync(memoryStream);
        var audioContent = memoryStream.ToArray();

        var recognitionAudio = RecognitionAudio.FromBytes(audioContent);
        var recognitionConfig = new RecognitionConfig
        {
            Encoding = RecognitionConfig.Types.AudioEncoding.Mp3,
            SampleRateHertz = 16000,
            LanguageCode = "en-US"
        };

        var speechClient = new SpeechClientBuilder()
        {
            CredentialsPath = _googleCredentialOptions.FilePath
        }.Build();
        var response = await speechClient.RecognizeAsync(recognitionConfig, recognitionAudio);

        var transcript = string.Join(
            " ",
            response.Results.Select(r => r.Alternatives.First().Transcript)
        );

        return transcript;
    }
}
