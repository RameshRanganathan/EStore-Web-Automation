using OpenQA.Selenium;
using Test_Store_Automation.Utils;
using Test_Store_Automation.Pages;
using FluentAssertions;
using NUnit.Framework;

namespace Test_Store_Automation.Tests
{
    [TestFixture]
    public class LoginTests : BaseTest
    {
        private LoginPage loginPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.CreateDriver();
            loginPage = new LoginPage(driver);
            test = ExtentReportManager.Instance.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Login_With_Valid_Credentials_Should_Succeed()
        {
            if (test == null)
            {
                throw new Exception("Test object is null");
            }

            test.Info("Navigating to login page");
            loginPage.GoTo();

            // Replace with valid credentials for your test environment
            string validUsername = "rameshpr"; // <-- Use a real username
            string validPassword = "Laki@2021"; // <-- Use a real password

            test.Info("Entering valid credentials and logging in");
            loginPage.Login(validUsername, validPassword);

            // Assert: Should redirect to account page (success)
            test.Info("Asserting successful login by checking URL");
            driver?.Url.Should().Contain("account/account");
        }

        [Test]
        public void Login_With_Invalid_Credentials_Should_Show_Error()
        {
            if (test == null)
            {
                throw new Exception("Test object is null");
            }

            test.Info("Navigating to login page");
            loginPage.GoTo();

            string invalidUsername = "invalidUser";
            string invalidPassword = "invalidPass";

            test.Info("Entering invalid credentials and attempting login");
            loginPage.Login(invalidUsername, invalidPassword);

            // Assert: Error message is shown
            test.Info("Asserting error message is displayed");
            loginPage.GetWarningMessage().Should().NotBeNullOrWhiteSpace();
        }
    }
}