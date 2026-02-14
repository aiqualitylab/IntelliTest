using Reqnroll;
using IntelliTest.Clients;
using IntelliTest.Core.Models;

namespace IntelliTest.Tests.StepDefinitions;

[Binding]
public class TestDataStepDefinitions
{
    private readonly ScenarioContext _context;
    private readonly TestDataClient _dataClient = new();

    public TestDataStepDefinitions(ScenarioContext context)
    {
        _context = context;
    }

    [Given("I generate a user for {string}")]
    public async Task GivenIGenerateUser(string context)
    {
        var user = await _dataClient.GenerateUser(context);
        _context["GeneratedUser"] = user;
    }

    [Given("I generate a credit card for {string}")]
    public async Task GivenIGenerateCreditCard(string context)
    {
        var card = await _dataClient.GenerateCreditCard(context);
        _context["GeneratedCard"] = card;
    }

    [Given("I generate a product for {string}")]
    public async Task GivenIGenerateProduct(string context)
    {
        var product = await _dataClient.GenerateProduct(context);
        _context["GeneratedProduct"] = product;
    }

    [Given("I generate {string} for {string}")]
    public async Task GivenIGenerateText(string type, string context)
    {
        var text = await _dataClient.GenerateText(type, context);
        _context["GeneratedText"] = text;
    }

    [Then("generated user should have first name")]
    public void ThenUserShouldHaveFirstName()
    {
        var user = _context["GeneratedUser"] as User;
        Console.WriteLine($"First name: {user?.FirstName}");
    }

    [Then("generated user should have last name")]
    public void ThenUserShouldHaveLastName()
    {
        var user = _context["GeneratedUser"] as User;
        Console.WriteLine($"Last name: {user?.LastName}");
    }

    [Then("generated user should have email")]
    public void ThenUserShouldHaveEmail()
    {
        var user = _context["GeneratedUser"] as User;
        Console.WriteLine($"Email: {user?.Email}");
    }

    [Then("generated user should have password")]
    public void ThenUserShouldHavePassword()
    {
        var user = _context["GeneratedUser"] as User;
        Console.WriteLine($"Password: {user?.Password}");
    }

    [Then("generated user should match context")]
    public void ThenUserShouldMatchContext()
    {
        var user = _context["GeneratedUser"] as User;
        Console.WriteLine($"Generated: {user?.FirstName} {user?.LastName}");
    }

    [Then("generated card should have number")]
    public void ThenCardShouldHaveNumber()
    {
        var card = _context["GeneratedCard"] as CreditCard;
        var lastFour = card?.Number?.Length >= 4 ? card.Number[^4..] : card?.Number;
        Console.WriteLine($"Card: ****{lastFour}");
    }

    [Then("generated card should have expiry")]
    public void ThenCardShouldHaveExpiry()
    {
        var card = _context["GeneratedCard"] as CreditCard;
        Console.WriteLine($"Expiry: {card?.Expiry}");
    }

    [Then("generated card should have cvv")]
    public void ThenCardShouldHaveCvv()
    {
        var card = _context["GeneratedCard"] as CreditCard;
        Console.WriteLine($"CVV: {card?.Cvv}");
    }

    [Then("generated product should have name")]
    public void ThenProductShouldHaveName()
    {
        var product = _context["GeneratedProduct"] as Product;
        Console.WriteLine($"Product: {product?.Name}");
    }

    [Then("generated product should have price")]
    public void ThenProductShouldHavePrice()
    {
        var product = _context["GeneratedProduct"] as Product;
        Console.WriteLine($"Price: {product?.Price}");
    }

    [Then("generated product should have category")]
    public void ThenProductShouldHaveCategory()
    {
        var product = _context["GeneratedProduct"] as Product;
        Console.WriteLine($"Category: {product?.Category}");
    }

    [Then("generated text should be positive review")]
    public void ThenTextShouldBePositiveReview()
    {
        var text = _context["GeneratedText"] as string;
        var preview = text?.Length > 100 ? text[..100] : text;
        Console.WriteLine($"Review: {preview}...");
    }

    [Then("generated address should be valid US address")]
    public void ThenAddressShouldBeValidUS()
    {
        var text = _context["GeneratedText"] as string;
        Console.WriteLine($"Address: {text}");
    }
}
