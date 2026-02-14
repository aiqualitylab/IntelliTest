using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Reqnroll;

namespace IntelliTest.Tests.Support;

[Binding]
public class Hooks
{
    private readonly ScenarioContext _context;
    private IWebDriver? _driver;

    public Hooks(ScenarioContext context)
    {
        _context = context;
    }

    [BeforeScenario("web")]
    public void SetupBrowser()
    {
        Console.WriteLine("Starting browser...");

        var options = new ChromeOptions();
        options.AddArgument("--disable-blink-features=AutomationControlled");

        var remoteUrl = Environment.GetEnvironmentVariable("SELENIUM_REMOTE_URL");

        _driver = string.IsNullOrEmpty(remoteUrl)
            ? new ChromeDriver(options)
            : new RemoteWebDriver(new Uri(remoteUrl), options);

        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        _context["Driver"] = _driver;

        Console.WriteLine("Browser ready!");
    }

    [AfterScenario("web")]
    public void CloseBrowser()
    {
        Console.WriteLine("Closing browser...");
        _driver?.Quit();
    }
}
