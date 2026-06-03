using OpenQA.Selenium;

namespace Test_Store_Automation.Utils
{
    internal class RegisterPage : PageBase
    {
        // Locators for all registration fields
        private readonly By firstNameInput = By.Id("AccountFrm_firstname");
        private readonly By lastNameInput = By.Id("AccountFrm_lastname");
        private readonly By emailInput = By.Id("AccountFrm_email");
        private readonly By telephoneInput = By.Id("AccountFrm_telephone");
        private readonly By faxInput = By.Id("AccountFrm_fax");
        private readonly By companyInput = By.Id("AccountFrm_company");
        private readonly By address1Input = By.Id("AccountFrm_address_1");
        private readonly By address2Input = By.Id("AccountFrm_address_2");
        private readonly By cityInput = By.Id("AccountFrm_city");
        private readonly By regionDropdown = By.Id("AccountFrm_zone_id");
        private readonly By zipInput = By.Id("AccountFrm_postcode");
        private readonly By countryDropdown = By.Id("AccountFrm_country_id");
        private readonly By loginNameInput = By.Id("AccountFrm_loginname");
        private readonly By passwordInput = By.Id("AccountFrm_password");
        private readonly By passwordConfirmInput = By.Id("AccountFrm_confirm");
        private readonly By newsletterYesRadio = By.CssSelector("input[name='newsletter'][value='1']");
        private readonly By newsletterNoRadio = By.CssSelector("input[name='newsletter'][value='0']");
        private readonly By agreeCheckbox = By.Name("agree");
        private readonly By continueButton = By.CssSelector("button[title='Continue']");
        private readonly By errorMessage = By.CssSelector(".alert-danger");

        public RegisterPage(IWebDriver driver) : base(driver) { }

        public void GoTo()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=account/create");
        }

        public void EnterFirstName(string firstName) => SendText(firstNameInput, firstName);
        public void EnterLastName(string lastName) => SendText(lastNameInput, lastName);
        public void EnterEmail(string email) => SendText(emailInput, email);
        public void EnterTelephone(string telephone) => SendText(telephoneInput, telephone);
        public void EnterFax(string fax) => SendText(faxInput, fax);
        public void EnterCompany(string company) => SendText(companyInput, company);
        public void EnterAddress1(string address1) => SendText(address1Input, address1);
        public void EnterAddress2(string address2) => SendText(address2Input, address2);
        public void EnterCity(string city) => SendText(cityInput, city);
        public void SelectRegion(string region) => SelectDropdownByText(regionDropdown, region);
        public void EnterZip(string zip) => SendText(zipInput, zip);
        public void SelectCountry(string country) => SelectDropdownByText(countryDropdown, country);
        public void EnterLoginName(string loginName) => SendText(loginNameInput, loginName);
        public void EnterPassword(string password) => SendText(passwordInput, password);
        public void EnterPasswordConfirm(string password) => SendText(passwordConfirmInput, password);

        public void SetNewsletter(bool subscribe)
        {
            if (subscribe)
                Click(newsletterYesRadio);
            else
                Click(newsletterNoRadio);
        }

        public void AgreeToPrivacyPolicy() => Click(agreeCheckbox);

        public void ClickContinue() => Click(continueButton);

        public string GetErrorMessage() => GetText(errorMessage);

        // Optional: Fill all fields at once
        public void Register(
            string firstName, string lastName, string email, string telephone, string fax,
            string company, string address1, string address2, string city, string region,
            string zip, string country, string loginName, string password, bool subscribe)
        {
            EnterFirstName(firstName);
            EnterLastName(lastName);
            EnterEmail(email);
            EnterTelephone(telephone);
            EnterFax(fax);
            EnterCompany(company);
            EnterAddress1(address1);
            EnterAddress2(address2);
            EnterCity(city);
            SelectCountry(country);
            SelectRegion(region);
            EnterZip(zip);            
            EnterLoginName(loginName);
            EnterPassword(password);
            EnterPasswordConfirm(password);
            SetNewsletter(subscribe);
            AgreeToPrivacyPolicy();
            ClickContinue();
        }
    }
}
