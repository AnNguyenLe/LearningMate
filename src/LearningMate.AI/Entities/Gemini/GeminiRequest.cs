namespace LearningMate.AI.Entities.Gemini;

public class GeminiRequest
{
    public GeminiContent[]? Contents { get; set; }
    public GenerationConfig? GenerationConfig { get; set; }
    public SafetySetting[]? SafetySettings { get; set; }
}
