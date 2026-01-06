namespace McpServerSample.Config;

public static class ApiKeyManager
{
    private static List<string> _keyList = new();
    public static void Initialize(IConfiguration configuration)
    {
        var apiKeysSection = configuration.GetSection("ApiKey");
        var apiKeys = apiKeysSection.Get<string[]>();
        _keyList = apiKeys?.ToList() ?? new List<string>();
    }

    public static bool IsValidApiKey(string apiKey)
    {
        return _keyList.Contains(apiKey);
    }
}