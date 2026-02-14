Feature: AI API Assertions

Scenario: Assert status code
    When I GET "https://jsonplaceholder.typicode.com/users/1"
    Then response should be "Status code is 200"

Scenario: Assert response contains data
    When I GET "https://jsonplaceholder.typicode.com/users/1"
    Then response should be "Response contains user data"
    And response should be "User has name field"
    And response should be "User has email field"

Scenario: Assert POST creates resource
    When I POST "https://jsonplaceholder.typicode.com/posts" with title "Test" and body "Content"
    Then response should be "Status code is 201"
    And response should be "Response contains title Test"

Scenario: Assert DELETE works
    When I DELETE "https://jsonplaceholder.typicode.com/posts/1"
    Then response should be "Status code is 200"

Scenario: Assert list response
    When I GET "https://jsonplaceholder.typicode.com/users"
    Then response should be "Response is an array"
    And response should be "Array has multiple users"
