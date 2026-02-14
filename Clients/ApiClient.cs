using System.Text;
using System.Text.Json;
using IntelliTest.Core.Services;

namespace IntelliTest.Clients;

public class ApiClient
{
    private readonly HttpClient _http = new();
    private readonly AIService _ai = new();

    public int StatusCode { get; private set; }
    public string Body { get; private set; } = "";

    public async Task Get(string url)
    {
        var response = await _http.GetAsync(url);
        await SaveResponse(response);
    }

    public async Task Post(string url, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);
        await SaveResponse(response);
    }

    public async Task Delete(string url)
    {
        var response = await _http.DeleteAsync(url);
        await SaveResponse(response);
    }

    public async Task Assert(string expectation)
    {
        var question = $@"Is this TRUE or FALSE?

Statement: {expectation}

API Response:
- Status: {StatusCode}
- Body: {Body}

Reply ONLY: {{ ""passed"": true/false, ""reason"": ""why"" }}";

        var answer = await _ai.Ask(question);
        var result = _ai.ParseResult(answer);

        if (!result.Passed) throw new Exception(result.Reason);
    }

    private async Task SaveResponse(HttpResponseMessage response)
    {
        StatusCode = (int)response.StatusCode;
        Body = await response.Content.ReadAsStringAsync();
    }
}
