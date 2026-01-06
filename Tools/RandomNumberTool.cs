using ModelContextProtocol.Server;
using Serilog;
using System.ComponentModel;

namespace McpServerSample.Tools;

[McpServerToolType]
public sealed class RandomNumberTool
{
    [McpServerTool, Description("Generate a random number within a specified range")]
    public string GetRandomNumber(
        [Description("Minimum value (inclusive)")] int min,
        [Description("Maximum value (exclusive)")] int max)
    {

        var random = new Random();
        var randomNumber = random.Next(min, max);
        Log.Information("Generated random number: {RandomNumber} (between {Min} and {Max})", randomNumber, min, max);
        return $"Generated random number: {randomNumber} (between {min} and {max})";
    }
}