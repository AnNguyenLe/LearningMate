using FluentResults;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.TextToSpeech.V1;
using LearningMate.Core.ConfigurationOptions.GCP;
using LearningMate.TextToSpeech.ServiceContracts;
using Microsoft.Extensions.Options;

namespace LearningMate.TextToSpeech.Services.GCP;

public class GCPTextToSpeechService(IOptions<GoogleCredentialOptions> googleCredentialOptions)
    : ITextToSpeechService
{
    private readonly GoogleCredentialOptions _googleCredential = googleCredentialOptions.Value;

    public async Task<Result<MemoryStream>> SynthesizeAsync(string sampleText)
    {
        var credential = GoogleCredential.FromFile(_googleCredential.FilePath);

        var ttsClientBuilder = new TextToSpeechClientBuilder { GoogleCredential = credential };

        var client = ttsClientBuilder.Build();

        var input = new SynthesisInput { Text = sampleText };

        var voiceSelection = new VoiceSelectionParams
        {
            LanguageCode = "en-US",
            SsmlGender = SsmlVoiceGender.Female
        };

        var audioConfig = new AudioConfig { AudioEncoding = AudioEncoding.Mp3 };

        var response = await client.SynthesizeSpeechAsync(input, voiceSelection, audioConfig);

        var audioStream = new MemoryStream(response.AudioContent.ToByteArray());
        return audioStream;
    }
}
