Feature: User Registration
  As a new customer
  I want to register an account
  So that I can access my account features

  Background:
    Given I am on the registration page

  Scenario: Successful registration with valid details
    When I enter valid registration details
    And I submit the registration form
    Then my account should be created successfully

  Scenario: Registration with missing required fields
    When I submit the registration form without filling required fields
    Then an error message should be displayed

  Scenario: Registration with existing username
    When I enter an already registered username
    And I submit the registration form
    Then a duplicate username error should be displayed
