# IntelliTest 🤖

[![CI](https://github.com/aiqualitylab/IntelliTest/actions/workflows/ci.yml/badge.svg)](https://github.com/aiqualitylab/IntelliTest/actions/workflows/ci.yml)

AI-powered test automation that lets you write tests in plain English. No complex code required.

## What is IntelliTest?

IntelliTest combines AI (OpenAI GPT) with test automation. Write your test assertions in natural language, and AI validates them for you.

**Example:**
```gherkin
When I GET "https://api.example.com/users/1"
Then response should be "Status code is 200"
And response should be "Response contains user data"
```

No need to write complex validation code - just describe what you want to check.

## Quick Start

### 1. Prerequisites
- .NET 9 SDK
- OpenAI API Key

### 2. Setup

Clone and configure:
```bash
git clone https://github.com/aiqualitylab/IntelliTest.git
cd IntelliTest
```

Add your OpenAI API key to `appsettings.json`:
```json
{
  "Model": "gpt-4o-mini",
  "ApiKey": "your-api-key-here",
  "MaxTokens": 2000
}
```

### 3. Run Tests
```bash
dotnet test
```

## Features

### 🔹 API Testing
Test APIs using natural language assertions:
```gherkin
Scenario: Validate user endpoint
    When I GET "https://jsonplaceholder.typicode.com/users/1"
    Then response should be "Status code is 200"
    And response should be "User has name and email"
```

### 🔹 Web Testing
Check web pages without writing selectors:
```gherkin
Scenario: Check Google homepage
    Given I open "https://www.google.com"
    Then page should have "Google logo present"
    And page should have "Search box visible"
```

### 🔹 Test Data Generation
Generate realistic test data with AI:
```gherkin
Scenario: Generate test user
    Given I generate a user for "registration test"
    Then generated user should have first name
    And generated user should have email
```

You can also create context-specific data:
```gherkin
Given I generate a user for "elderly customer over 65 years old"
Given I generate a product for "luxury electronics"
```

## Project Structure

```
IntelliTest/
├── Core/
│   ├── Services/AIService.cs          # AI integration
│   └── Models/                        # Data models
├── Clients/
│   ├── ApiClient.cs                   # API testing
│   ├── WebClient.cs                   # Web testing
│   └── TestDataClient.cs              # Data generation
└── Tests/
    ├── Features/                      # Test scenarios (.feature files)
    └── StepDefinitions/               # Test implementations
```

## Configuration

Edit `appsettings.json` to configure:

```json
{
  "Model": "gpt-4o-mini",      // AI model to use
  "ApiKey": "",                 // Your OpenAI API key
  "MaxTokens": 2000,           // Max response length
  "Temperature": 0.1            // 0=consistent, 1=creative
}
```

## How It Works

IntelliTest uses:
- **Semantic Kernel** - Microsoft's AI orchestration framework
- **OpenAI GPT-4o-mini** - For understanding natural language
- **Reqnroll** - BDD framework for writing tests
- **NUnit** - Test runner
- **Selenium** - Web browser automation

When you write an assertion like "Status code is 200", the AI analyzes the actual response and validates it matches your expectation.

## Tips for Writing Tests

✅ **Do:**
- Keep assertions simple and focused
- Use clear, descriptive language
- One concept per assertion

❌ **Don't:**
- Make assertions too complex
- Mix multiple checks in one assertion
- Use vague descriptions

## Troubleshooting

**"401 Unauthorized" error**  
→ Check your API key is correct in `appsettings.json`

**"Model does not exist" error**  
→ Verify your OpenAI account has access to the model

**Tests are slow**  
→ Normal - AI responses take 1-3 seconds per assertion

---

**Built with Semantic Kernel, OpenAI, and Reqnroll**
