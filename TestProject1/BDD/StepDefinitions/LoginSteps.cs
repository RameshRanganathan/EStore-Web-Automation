using OpenQA.Selenium;
using FluentAssertions;
using Reqnroll;

using Test_Store_Automation.Pages;

namespace Test_Store_Automation.BDD.StepDefinitions
{
    [Binding]
    internal class LoginSteps
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginPage;

        public LoginSteps(IWebDriver driver)
        {
            this.driver = driver;
            loginPage = new LoginPage(driver);
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage() => loginPage.GoTo();

        [When(@"I enter username ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterUsernameAndPassword(string username, string password) =>
            loginPage.Login(username, password);

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton() => loginPage.ClickLogin();

        [Then(@"I should be redirected to the account page")]
        public void ThenIShouldBeRedirectedToTheAccountPage() =>
            driver.Url.Should().Contain("account/account");

        [Then(@"I should see a warning message")]
        public void ThenIShouldSeeAWarningMessage() =>
            loginPage.GetWarningMessage().Should().NotBeNullOrWhiteSpace();
    }
}
