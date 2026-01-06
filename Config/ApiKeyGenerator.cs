using System.Security.Cryptography;

namespace McpServerSample.Config;

public static class ApiKeyGenerator
{
    private const string Prefix = "sk_";
    private const int KeyLength = 32;

    public static string GenerateApiKey()
    {
        var bytes = RandomNumberGenerator.GetBytes(KeyLength);
        var key = Convert.ToBase64String(bytes)
            .Replace("/", "_")
            .Replace("+", "-")
            .TrimEnd('=');
        return $"{Prefix}{key}";
    }

    public static bool IsValidApiKeyFormat(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
            return false;

        if (!apiKey.StartsWith(Prefix))
            return false;

        var keyPart = apiKey.Substring(Prefix.Length);
        return keyPart.Length >= 32;
    }
}
