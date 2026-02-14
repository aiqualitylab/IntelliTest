@web
Feature: AI Web Assertions

Scenario: Assert page has text
    Given I open "https://www.wikipedia.org"
    Then page should have "Text about free encyclopedia"
    And page should have "Multiple language options"

Scenario: Assert page structure
    Given I open "https://www.google.com"
    Then page should have "Google logo present"
    And page should have "Search box visible"
    And page should have "Search buttons present"
