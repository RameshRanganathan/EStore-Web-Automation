using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class SkinCare_Tests : BaseTest
    {
        private SkinCarePage skinCarePage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            var loginPage = new LoginPage(driver);
            loginPage.LoginAndNavigateToAccount("rameshpr", "Laki@2021");

            skinCarePage = new SkinCarePage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to SkinCare category page");
            skinCarePage.GoTo();
        }

        [Test]
        public void SkinCare_Add_First_Product_Should_Show_Success()
        {
            skinCarePage.AddFirstProductToCart();
            skinCarePage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void SkinCare_Click_Product_By_Name_Should_Open_Product()
        {
            var products = skinCarePage.GetProductNames();
            products.Should().NotBeEmpty();

            skinCarePage.ClickProductByName(products[0]);
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void SkinCare_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            FluentActions.Invoking(() => skinCarePage.AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void SkinCare_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            var products = skinCarePage.GetProductNames();
            products.Should().NotContain(name => name.Contains("NoSuchSkinCareProductXYZ"));
        }
    }
}
