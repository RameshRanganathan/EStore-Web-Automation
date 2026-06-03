using OpenQA.Selenium;
using Test_Store_Automation.Utils;

namespace Test_Store_Automation.Pages
{
    internal class LoginPage : PageBase
    {
        // Locators for login page elements
        private readonly By usernameInput = By.Id("loginFrm_loginname");
        private readonly By passwordInput = By.Id("loginFrm_password");
        private readonly By loginButton = By.CssSelector("button[title='Login']");
        private readonly By forgotPasswordLink = By.LinkText("Forgot your password?");
        private readonly By continueButton = By.CssSelector("a[title='Continue']");
        private readonly By warningMessage = By.CssSelector(".alert-danger");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void GoTo()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/login");
        }

        public void EnterUsername(string username) => SendText(usernameInput, username);

        public void EnterPassword(string password) => SendText(passwordInput, password);

        public void ClickLogin() => Click(loginButton);

        public void ClickForgotPassword() => Click(forgotPasswordLink);

        public void ClickContinue() => Click(continueButton);

        public string GetWarningMessage() => GetText(warningMessage);

        /// <summary>
        /// Performs a login with the provided credentials.
        /// </summary>
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public void LoginAndNavigateToAccount(string username, string password)
        {
            GoTo();
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
    }
}