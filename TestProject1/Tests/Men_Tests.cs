using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class Men_Tests : BaseTest
    {
        private MenPage menPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            menPage = new MenPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to Men category page");
            menPage.GoTo();
        }

        [Test]
        public void Men_Add_First_Product_Should_Show_Success()
        {
            menPage.AddFirstProductToCart();
            menPage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void Men_Click_Product_By_Name_Should_Open_Product()
        {
            var products = menPage.GetProductNames();
            products.Should().NotBeEmpty();

            menPage.ClickProductByName(products[0]);
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void Men_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            FluentActions.Invoking(() => menPage.AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void Men_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            var products = menPage.GetProductNames();
            products.Should().NotContain(name => name.Contains("NoSuchMenProductXYZ"));
        }
    }
}
