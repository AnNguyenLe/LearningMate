using System.Net.Mime;
using System.Text;
using System.Text.Json;
using FluentResults;
using LearningMate.AI.ConfigOptions;
using LearningMate.AI.Entities.Gemini;
using LearningMate.AI.ServiceContracts.PromptServiceContract;
using LearningMate.Core.Errors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LearningMate.AI.Services.PromptingService.GoogleGemini;

public class GoogleGeminiPromptService(
    ILogger<GoogleGeminiPromptService> logger,
    IHttpClientFactory httpClientFactory,
    IOptions<GeminiFlashOptions> geminiFlashOptions
) : IPromptService
{
    private readonly ILogger<GoogleGeminiPromptService> _logger = logger;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly GeminiFlashOptions _geminiFlashOptions = geminiFlashOptions.Value;

    public async Task<Result<string>> GenerateContent(string prompt)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var body = new GeminiRequest
        {
            Contents = [new GeminiContent() { Parts = [new GeminiPart() { Text = prompt }] }]
        };

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var requestBody = JsonSerializer.Serialize(body, jsonSerializerOptions);

        var content = new StringContent(
            requestBody,
            Encoding.UTF8,
            MediaTypeNames.Application.Json
        );

        var url = $"{_geminiFlashOptions.BaseUrl}";
        httpClient.DefaultRequestHeaders.Add("x-goog-api-key", $"{_geminiFlashOptions.ApiKey}");
        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();

        var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(
            result,
            jsonSerializerOptions
        );

        string geminiResponseText;

        try
        {
            geminiResponseText = geminiResponse?.Candidates?[0].Content?.Parts?[0].Text!;
        }
        catch (NullReferenceException)
        {
            _logger.LogError("Failed to get response from Gemini Flash");
            return new ProblemDetailsError("Failed to get response from Gemini Flash");
        }

        return geminiResponseText!;
    }
}
