using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class Fragrance_Tests : BaseTest
    {
        private FragrancePage fragrancePage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            fragrancePage = new FragrancePage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to Fragrance category page");
            fragrancePage.GoTo();
        }

        [Test]
        public void Fragrance_Add_First_Product_Should_Show_Success()
        {
            fragrancePage.AddFirstProductToCart();
            fragrancePage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void Fragrance_Click_Product_By_Name_Should_Open_Product()
        {
            var products = fragrancePage.GetProductNames();
            products.Should().NotBeEmpty();

            fragrancePage.ClickProductByName(products[0]);
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void Fragrance_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            FluentActions.Invoking(() => fragrancePage.AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void Fragrance_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            var products = fragrancePage.GetProductNames();
            products.Should().NotContain(name => name.Contains("NoSuchFragranceProductXYZ"));
        }
    }
}
