Feature: Product Category
  As a customer
  I want to browse and add products
  So that I can purchase them successfully

  Scenario Outline: Add first product to cart
    Given I am on the <Category> category page
    When I add the first product to the cart in <Category>
    Then a success alert should be displayed

  Scenario Outline: Click product by name
    Given I am on the <Category> category page
    When I click a product by name in <Category>
    Then the product detail page should open

  Scenario Outline: Add product with invalid index
    Given I am on the <Category> category page
    When I try to add a product with invalid index in <Category>
    Then an error should be thrown

  Scenario Outline: Search for nonexistent product
    Given I am on the <Category> category page
    When I fetch product names in <Category>
    Then no product should contain "NoSuch<Category>ProductXYZ"

  Examples:
    | Category   |
    | Books      |
    | Apparel    |
    | Fragrance  |
    | HairCare   |
    | MakeUp     |
    | Men        |
    | SkinCare   |
