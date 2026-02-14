using Reqnroll;
using IntelliTest.Clients;

namespace IntelliTest.Tests.StepDefinitions;

[Binding]
public class ApiStepDefinitions
{
    private readonly ScenarioContext _context;
    private readonly ApiClient _api = new();

    public ApiStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [When("I GET {string}")]
    public async Task WhenIGet(string url)
    {
        await _api.Get(url);
        _context["ApiBody"] = _api.Body;
        _context["ApiStatus"] = _api.StatusCode;
    }

    [When("I POST {string} with title {string} and body {string}")]
    public async Task WhenIPost(string url, string title, string body)
    {
        await _api.Post(url, new { title, body, userId = 1 });
        _context["ApiBody"] = _api.Body;
    }

    [When("I DELETE {string}")]
    public async Task WhenIDelete(string url)
    {
        await _api.Delete(url);
    }

    [Then("response should be {string}")]
    public async Task ThenResponseShouldBe(string expectation)
    {
        await _api.Assert(expectation);
    }
}
