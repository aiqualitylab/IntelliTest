namespace IntelliTest.Core.Services;

public class ConfigurationService
{
    public string Model { get; set; } = "gpt-4o-mini";
    
    private string _apiKey = "";
    public string ApiKey
    {
        get => Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? _apiKey;
        set => _apiKey = value;
    }
    
    public double Temperature { get; set; } = 0.1;
    public int MaxTokens { get; set; } = 2000;
}
