Feature: AI Test Data Generator

Scenario: Generate user for registration
    Given I generate a user for "registration test"
    Then generated user should have first name
    And generated user should have last name
    And generated user should have email
    And generated user should have password

Scenario: Generate user for specific scenario
    Given I generate a user for "elderly customer over 65 years old"
    Then generated user should match context

Scenario: Generate product data
    Given I generate a product for "electronics category"
    Then generated product should have name
    And generated product should have price
    And generated product should have category

Scenario: Generate custom text
    Given I generate "product review" for "5-star positive review"
    Then generated text should be positive review

Scenario: Generate address
    Given I generate "shipping address" for "US customer"
    Then generated address should be valid US address
