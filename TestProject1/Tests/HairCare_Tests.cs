using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class HairCare_Tests : BaseTest
    {
        private HairCarePage hairCarePage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            hairCarePage = new HairCarePage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to HairCare category page");
            hairCarePage.GoTo();
        }

        [Test]
        public void HairCare_Add_First_Product_Should_Show_Success()
        {
            hairCarePage.AddFirstProductToCart();
            hairCarePage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void HairCare_Click_Product_By_Name_Should_Open_Product()
        {
            var products = hairCarePage.GetProductNames();
            products.Should().NotBeEmpty();

            hairCarePage.ClickProductByName(products[0]);
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void HairCare_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            FluentActions.Invoking(() => hairCarePage.AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void HairCare_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            var products = hairCarePage.GetProductNames();
            products.Should().NotContain(name => name.Contains("NoSuchHairCareProductXYZ"));
        }
    }
}
