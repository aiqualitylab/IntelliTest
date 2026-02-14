using OpenQA.Selenium;
using IntelliTest.Core.Services;

namespace IntelliTest.Clients;

public class WebClient
{
    private readonly IWebDriver _driver;
    private readonly AIService _ai = new();

    public WebClient(IWebDriver driver) => _driver = driver;

    public void GoTo(string url) => _driver.Navigate().GoToUrl(url);

    public void Type(string id, string text) => 
        _driver.FindElement(By.Id(id)).SendKeys(text);

    public void Click(string id) => 
        _driver.FindElement(By.Id(id)).Click();

    public void Submit(string id) => 
        _driver.FindElement(By.Id(id)).SendKeys(Keys.Enter);

    public async Task Assert(string expectation)
    {
        var html = _driver.PageSource;
        var shortHtml = html.Length > 4000 ? html[..4000] : html;

        var question = $@"Is this TRUE or FALSE?

Statement: {expectation}

Page Title: {_driver.Title}
Page URL: {_driver.Url}

HTML:
{shortHtml}

Reply ONLY: {{ ""passed"": true/false, ""reason"": ""why"" }}";

        var answer = await _ai.Ask(question);
        var result = _ai.ParseResult(answer);

        if (!result.Passed) throw new Exception(result.Reason);
    }

    public async Task<string> AnalyzeScreenshot(string question)
    {
        var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
        var base64 = screenshot.AsBase64EncodedString;

        var prompt = $@"Look at this webpage screenshot and answer: {question}

Page Title: {_driver.Title}
Page URL: {_driver.Url}

Describe what you see and answer the question.";

        return await _ai.Ask(prompt);
    }

    public async Task<string> ExplainFailure()
    {
        var html = _driver.PageSource;
        var shortHtml = html.Length > 3000 ? html[..3000] : html;

        var question = $@"A test failed on this page. Explain what might be wrong.

Page Title: {_driver.Title}
Page URL: {_driver.Url}

HTML:
{shortHtml}

Look for:
- Error messages
- Disabled buttons
- Missing elements
- Form validation errors

Explain the likely cause of failure.";

        return await _ai.Ask(question);
    }

    public async Task SmartWait(string condition, int maxSeconds = 30)
    {
        var endTime = DateTime.Now.AddSeconds(maxSeconds);

        while (DateTime.Now < endTime)
        {
            await Task.Delay(1000);
            
            var html = _driver.PageSource;
            var shortHtml = html.Length > 2000 ? html[..2000] : html;

            var question = $@"Is this condition TRUE now? Answer only YES or NO.

Condition: {condition}

Page Title: {_driver.Title}
Page URL: {_driver.Url}

HTML:
{shortHtml}";

            var answer = await _ai.Ask(question);
            var isMet = answer.Trim().ToUpper().StartsWith("YES");
            
            if (isMet) return;
        }

        throw new TimeoutException($"Timeout waiting for: {condition}");
    }
}
