using System.Text.Json;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace IntelliTest.Core.Services;

public class AIService
{
    private readonly IChatCompletionService _chat;
    private readonly OpenAIPromptExecutionSettings _settings;

    public AIService()
    {
        var config = JsonSerializer.Deserialize<ConfigurationService>(File.ReadAllText("appsettings.json"))!;

        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(config.Model, config.ApiKey)
            .Build();

        _chat = kernel.GetRequiredService<IChatCompletionService>();
        _settings = new OpenAIPromptExecutionSettings
        {
            Temperature = config.Temperature,
            MaxTokens = config.MaxTokens
        };
    }

    public async Task<string> Ask(string question)
    {
        var history = new ChatHistory();
        history.AddUserMessage(question);

        var response = await _chat.GetChatMessageContentAsync(history, _settings);
        return response.Content ?? string.Empty;
    }

    public (bool Passed, string Reason) ParseResult(string aiResponse)
    {
        var clean = aiResponse.Replace("```json", "").Replace("```", "").Trim();
        var json = JsonDocument.Parse(clean).RootElement;

        var passed = json.TryGetProperty("passed", out var p) && p.GetBoolean();
        var reason = json.TryGetProperty("reason", out var r) ? r.GetString() ?? "" : "";

        return (passed, reason);
    }
}
