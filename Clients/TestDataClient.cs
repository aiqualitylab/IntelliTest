using System.Text.Json;
using IntelliTest.Core.Services;
using IntelliTest.Core.Models;

namespace IntelliTest.Clients;

public class TestDataClient
{
    private readonly AIService _ai = new();

    public async Task<User> GenerateUser(string context = "")
    {
        var question = $@"Generate realistic test user data {context}.

Reply with ONLY JSON:
{{
  ""firstName"": ""..."",
  ""lastName"": ""..."",
  ""email"": ""..."",
  ""phone"": ""..."",
  ""password"": ""..."",
  ""address"": ""..."",
  ""city"": ""..."",
  ""country"": ""...""
}}";

        var answer = await _ai.Ask(question);
        var clean = CleanJson(answer);
        return JsonSerializer.Deserialize<User>(clean)!;
    }

    public async Task<CreditCard> GenerateCreditCard(string context = "")
    {
        var question = $@"Generate test credit card data {context}. Use fake/test numbers only.

Reply with ONLY JSON:
{{
  ""number"": ""4111111111111111"",
  ""expiry"": ""12/25"",
  ""cvv"": ""123"",
  ""name"": ""...""
}}";

        var answer = await _ai.Ask(question);
        var clean = CleanJson(answer);
        return JsonSerializer.Deserialize<CreditCard>(clean)!;
    }

    public async Task<Product> GenerateProduct(string context = "")
    {
        var question = $@"Generate realistic product data {context}.

Reply with ONLY JSON:
{{
  ""name"": ""..."",
  ""description"": ""..."",
  ""price"": 0.00,
  ""category"": ""..."",
  ""sku"": ""...""
}}";

        var answer = await _ai.Ask(question);
        var clean = CleanJson(answer);
        return JsonSerializer.Deserialize<Product>(clean)!;
    }

    public async Task<string> GenerateText(string type, string context = "")
    {
        var question = $@"Generate realistic {type} {context}.

Reply with ONLY the text, nothing else.";

        var answer = await _ai.Ask(question);
        return answer.Trim();
    }

    private static string CleanJson(string response) =>
        response.Replace("```json", "").Replace("```", "").Trim();
}
