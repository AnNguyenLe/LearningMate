namespace LearningMate.AI.Entities.Gemini;

public class GeminiResponse
{
    public Candidate[]? Candidates { get; set; }
    public PromptFeedback? PromptFeedback { get; set; }
}
