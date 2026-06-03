Feature: Login functionality
  As a registered user
  I want to log in with valid and invalid credentials
  So that I can access my account or see an error message

  Scenario: Login with valid credentials
    Given I am on the login page
    When I enter username "demo" and password "demo"
    And I click the login button
    Then I should be redirected to the account page

  Scenario: Login with invalid credentials
    Given I am on the login page
    When I enter username "rameshpr" and password "Laki@2021"
    And I click the login button
    Then I should see a warning message