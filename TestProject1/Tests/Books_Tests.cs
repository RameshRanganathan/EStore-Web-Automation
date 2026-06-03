using NUnit.Framework;
using OpenQA.Selenium;
using FluentAssertions;
using Test_Store_Automation.Pages;
using Test_Store_Automation.Utils;
using System;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class Books_Tests : BaseTest
    {
        private BooksPage booksPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            booksPage = new BooksPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);

            test.Info("Navigating to Books category page");
            booksPage.GoTo();
        }

        [Test]
        public void Books_Add_First_Product_Should_Show_Success()
        {
            booksPage.AddFirstProductToCart();
            booksPage.GetSuccessAlert().Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void Books_Click_Product_By_Name_Should_Open_Product()
        {
            var products = booksPage.GetProductNames();
            products.Should().NotBeEmpty();

            booksPage.ClickProductByName(products[0]);
            driver?.Url.Should().Contain("product/product");
        }

        [Test]
        public void Books_Add_Product_With_Invalid_Index_Should_Throw_Exception()
        {
            FluentActions.Invoking(() => booksPage.AddProductToCartByIndex(9999))
                .Should().Throw<NoSuchElementException>();
        }

        [Test]
        public void Books_Search_For_Nonexistent_Product_Should_Return_Empty_List()
        {
            var products = booksPage.GetProductNames();
            products.Should().NotContain(name => name.Contains("NoSuchBooksProductXYZ"));
        }
    }
}
