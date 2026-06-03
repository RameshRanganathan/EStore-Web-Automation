using Reqnroll;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using FluentAssertions;
using Test_Store_Automation.Utils; // RegisterPage is in Utils namespace

namespace Test_Store_Automation.BDD.StepDefinitions
{
    [Binding]
    public class RegisterSteps
    {
        private readonly IWebDriver driver;
        private readonly ExtentTest test;
        private readonly RegisterPage registerPage;

        public RegisterSteps(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
            registerPage = new RegisterPage(driver);
        }

        [Given(@"I am on the registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            test.Info("Navigating to registration page");
            registerPage.GoTo();
        }

        [When(@"I enter valid registration details")]
        public void WhenIEnterValidRegistrationDetails()
        {
            test.Info("Entering valid registration details");
            registerPage.Register(
                "Ramesh", "Pr", "rameshpr" + DateTime.Now.Ticks + "@example.com", "1234567890", "",
                "TestCompany", "123 Street", "", "Chennai", "Tamil Nadu",
                "600001", "India", "rameshpr" + DateTime.Now.Ticks, "Laki@2021", true
            );
        }

        [When(@"I submit the registration form")]
        public void WhenISubmitTheRegistrationForm()
        {
            test.Info("Submitting registration form");
            registerPage.ClickContinue();
        }

        [Then(@"my account should be created successfully")]
        public void ThenMyAccountShouldBeCreatedSuccessfully()
        {
            // Adjust locator/method in RegisterPage to capture success message
            var successMessage = driver.PageSource;
            successMessage.Should().Contain("Your Account Has Been Created");
        }

        [When(@"I submit the registration form without filling required fields")]
        public void WhenISubmitTheRegistrationFormWithoutFillingRequiredFields()
        {
            test.Info("Submitting empty registration form");
            registerPage.ClickContinue();
        }

        [Then(@"an error message should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed()
        {
            registerPage.GetErrorMessage().Should().NotBeNullOrWhiteSpace();
        }

        [When(@"I enter an already registered username")]
        public void WhenIEnterAnAlreadyRegisteredUsername()
        {
            test.Info("Entering duplicate username");
            registerPage.Register(
                "Ramesh", "Pr", "duplicate@example.com", "1234567890", "",
                "TestCompany", "123 Street", "", "Chennai", "Tamil Nadu",
                "600001", "India", "rameshpr", "Laki@2021", true
            );
        }

        [Then(@"a duplicate username error should be displayed")]
        public void ThenADuplicateUsernameErrorShouldBeDisplayed()
        {
            registerPage.GetErrorMessage().Should().Contain("already exists");
        }
    }
}
