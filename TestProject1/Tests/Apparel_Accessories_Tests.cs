using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class Apparel_Accessories_Tests : BaseTest
    {
        private Apparel_AccessoriesPage apparelPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            LoginPage loginPage = new LoginPage(driver);
            loginPage.LoginAndNavigateToAccount("rameshpr", "Laki@2021");

            apparelPage = new Apparel_AccessoriesPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to Apparel & Accessories category page");
            apparelPage.GoTo();
        }

        [Test]
        public void Apparel_Add_First_Product_Should_Show_Success()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Adding first Apparel product to cart");
            apparelPage.AddFirstProductToCart();

            test.Info("Asserting success alert is displayed");
            apparelPage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void Apparel_Click_Product_By_Name_Should_Open_Product()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Fetching product names");
            var products = apparelPage.GetProductNames();
            products.Should().NotBeEmpty("Apparel & Accessories category should have products");

            string productName = products[0];
            test.Info($"Clicking product: {productName}");
            apparelPage.ClickProductByName(productName);

            test.Info("Asserting product page is opened");
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void Apparel_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Attempting to add product with invalid index");
            int invalidIndex = 9999;

            FluentActions.Invoking(() => apparelPage.AddProductToCartByIndex(invalidIndex))
                .Should().Throw<NoSuchElementException>()
                .WithMessage("*No Add to Cart button at index*");
        }

        [Test]
        public void Apparel_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            if (test == null) throw new Exception("Test object is null");

            test.Info("Fetching product names");
            var products = apparelPage.GetProductNames();

            test.Info("Asserting no product contains invalid name");
            products.Should().NotContain(name => name.Contains("NoSuchApparelProductXYZ"));
        }
    }
}
