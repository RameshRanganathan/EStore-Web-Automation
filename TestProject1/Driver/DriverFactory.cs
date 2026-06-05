using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Test_Store_Automation.Driver
{
    internal static class DriverFactory
    {
        internal static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }
    }
}
