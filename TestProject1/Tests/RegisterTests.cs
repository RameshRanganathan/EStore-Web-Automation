using OpenQA.Selenium;
using Test_Store_Automation.Utils;
using FluentAssertions;
using NUnit.Framework;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class RegisterTests : BaseTest
    {
        private RegisterPage registerPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            registerPage = new RegisterPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Register_With_Valid_Data_Should_Succeed()
        {
            if (test == null)
            {
                throw new Exception("Test object is null");
            }

            test.Info("Navigating to registration page");
            registerPage.GoTo();
            string uniqueEmail = $"user{System.DateTime.Now.Ticks}@test.com";
            string uniqueLogin = $"user{System.DateTime.Now.Ticks}";

            test.Info("Filling registration form with valid data");
            registerPage.Register(
                firstName: "John",
                lastName: "Doe",
                email: uniqueEmail,
                telephone: "1234567890",
                fax: "",
                company: "",
                address1: "123 Main St",
                address2: "",
                city: "New York",
                region: "New York",
                zip: "10001",
                country: "United States",
                loginName: uniqueLogin,
                password: "Password123!",
                subscribe: false
            );

            test.Info("Asserting registration success");
            driver?.Url.Should().Contain("account/success");
        }

        [Test]
        public void Register_With_Missing_Required_Fields_Should_Show_Error()
        {
            if (test == null)
            {
                throw new Exception("Test object is null");
            }

            test.Info("Navigating to registration page");
            registerPage.GoTo();

            test.Info("Filling registration form with missing required fields");
            registerPage.EnterFirstName("");
            registerPage.EnterLastName("");
            registerPage.EnterEmail("");
            registerPage.EnterTelephone("");
            registerPage.EnterAddress1("");
            registerPage.EnterCity("");
            registerPage.SelectCountry("United States");
            registerPage.SelectRegion("New York");
            registerPage.EnterZip("");
            registerPage.EnterLoginName("");
            registerPage.EnterPassword("");
            registerPage.EnterPasswordConfirm("");
            registerPage.SetNewsletter(false);
            registerPage.AgreeToPrivacyPolicy();
            registerPage.ClickContinue();

            test.Info("Asserting error message is shown");
            registerPage.GetErrorMessage().Should().NotBeNullOrWhiteSpace();
        }
    }
}