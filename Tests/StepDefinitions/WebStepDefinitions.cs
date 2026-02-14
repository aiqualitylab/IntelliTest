using OpenQA.Selenium;
using Reqnroll;
using IntelliTest.Clients;

namespace IntelliTest.Tests.StepDefinitions;

[Binding]
public class WebStepDefinitions
{
    private readonly ScenarioContext _context;
    private WebClient? _web;

    public WebStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    private IWebDriver Driver => (IWebDriver)_context["Driver"];
    private WebClient Web => _web ??= new WebClient(Driver);

    [Given("I open {string}")]
    public void GivenIOpen(string url)
    {
        Web.GoTo(url);
    }

    [When("I type {string} in {string}")]
    public void WhenIType(string text, string id)
    {
        Web.Type(id, text);
    }

    [When("I click {string}")]
    public void WhenIClick(string id)
    {
        Web.Click(id);
    }

    [When("I submit {string}")]
    public void WhenISubmit(string id)
    {
        Web.Submit(id);
    }

    [Then("page should have {string}")]
    public async Task ThenPageShouldHave(string expectation)
    {
        await Web.Assert(expectation);
    }
}
