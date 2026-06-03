using Reqnroll;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using FluentAssertions;
using Test_Store_Automation.Pages;
using System;

namespace Test_Store_Automation.BDD.StepDefinitions
{
    [Binding]
    public class ProductSteps
    {
        private readonly IWebDriver driver;
        private readonly ExtentTest test;
        private readonly ScenarioContext scenarioContext;

        public ProductSteps(IWebDriver driver, ExtentTest test, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            this.test = test;
            this.scenarioContext = scenarioContext;
        }

        private dynamic GetPage(string category)
        {
            switch (category.ToLower())
            {
                case "books": return new BooksPage(driver);
                case "apparel": return new Apparel_AccessoriesPage(driver);
                case "fragrance": return new FragrancePage(driver);
                case "haircare": return new HairCarePage(driver);
                case "makeup": return new MakeUpPage(driver);
                case "men": return new MenPage(driver);
                case "skincare": return new SkinCarePage(driver);
                default: throw new ArgumentException($"Unknown category: {category}");
            }
        }

        [Given(@"I am on the (.*) category page")]
        public void GivenIAmOnTheCategoryPage(string category)
        {
            test.Info($"Navigating to {category} category page");
            GetPage(category).GoTo();
        }

        [When(@"I add the first product to the cart in (.*)")]
        public void WhenIAddTheFirstProductToTheCart(string category)
        {
            GetPage(category).AddFirstProductToCart();
        }

        [Then(@"a success alert should be displayed")]
        public void ThenASuccessAlertShouldBeDisplayed()
        {
            driver.PageSource.Should().Contain("Success");
        }

        [When(@"I click a product by name in (.*)")]
        public void WhenIClickAProductByName(string category)
        {
            var page = GetPage(category);
            var products = page.GetProductNames();
            products.Should().NotBeEmpty();
            page.ClickProductByName(products[0]);
        }

        [Then(@"the product detail page should open")]
        public void ThenTheProductDetailPageShouldOpen()
        {
            driver.Url.Should().Contain("product/product");
        }

        [When(@"I try to add a product with invalid index in (.*)")]
        public void WhenITryToAddAProductWithInvalidIndex(string category)
        {
            FluentActions.Invoking(() => GetPage(category).AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [When(@"I fetch product names in (.*)")]
        public void WhenIFetchProductNames(string category)
        {
            scenarioContext["Products"] = GetPage(category).GetProductNames();
        }

        [Then(@"no product should contain ""(.*)""")]
        public void ThenNoProductShouldContain(string invalidName)
        {
            var products = (IList<string>)scenarioContext["Products"];
            products.Should().NotContain(name => name.Contains(invalidName));
        }
    }
}
