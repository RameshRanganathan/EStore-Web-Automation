using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class MakeUp_Tests : BaseTest
    {
        private MakeUpPage makeUpPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginAndNavigateToAccount("rameshpr", "Laki@2021");

            makeUpPage = new MakeUpPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to MakeUp category page");
            makeUpPage.GoTo();
        }

        // ✅ Positive Test: Add first product to cart
        [Test]
        public void MakeUp_Add_First_Product_Should_Show_Success()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Adding first MakeUp product to cart");
            makeUpPage.AddFirstProductToCart();

            test.Info("Asserting success alert is displayed");
            makeUpPage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        // ✅ Positive Test: Click product by name
        [Test]
        public void MakeUp_Click_Product_By_Name_Should_Open_Product()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Fetching product names");
            var products = makeUpPage.GetProductNames();
            products.Should().NotBeEmpty("MakeUp category should have products");

            string productName = products[0];
            test.Info($"Clicking product: {productName}");
            makeUpPage.ClickProductByName(productName);

            test.Info("Asserting product page is opened");
            driver?.Url.Should().Contain("product/product");
        }

        // ❌ Negative Test: Invalid product index
        [Test]
        public void MakeUp_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Attempting to add product with invalid index");
            int invalidIndex = 9999;

            FluentActions.Invoking(() => makeUpPage.AddProductToCartByIndex(invalidIndex))
                .Should().Throw<NoSuchElementException>()
                .WithMessage("*No Add to Cart button at index*");
        }

        // ❌ Negative Test: Nonexistent product name
        [Test]
        public void MakeUp_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Fetching product names");
            var products = makeUpPage.GetProductNames();

            test.Info("Asserting no product contains invalid name");
            products.Should().NotContain(name => name.Contains("NoSuchMakeUpProductXYZ"));
        }
    }
}
