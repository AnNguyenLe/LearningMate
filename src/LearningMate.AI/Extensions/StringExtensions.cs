namespace LearningMate.AI.Extensions;

public static class StringExtensions
{
    public static string RemoveJsonDelimiters(this string input)
    {
        var JSON_NOTATION_START = "```json";
        var JSON_NOTATION_END = "```";
        var totalCharactersAtTheBeginning = JSON_NOTATION_START.Length;
        var totalCharactersAtTheEnd = JSON_NOTATION_END.Length;
        string output = input;
        if (input.StartsWith(JSON_NOTATION_START))
        {
            output = input
                .Substring(
                    totalCharactersAtTheBeginning,
                    input.Length - (totalCharactersAtTheBeginning + totalCharactersAtTheEnd + 1)
                )
                .Trim();
        }

        return output;
    }
}
