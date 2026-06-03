using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test_Store_Automation.Utils
{
    internal abstract class PageBase
    {
        protected readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        protected PageBase(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        // 🔹 Reusable: Find a single web element with wait and scroll into view
        protected IWebElement FindWebElement(By locator)
        {
            try
            {
                var element = wait.Until(drv =>
                {
                    var el = drv.FindElement(locator);
                    return (el != null && el.Displayed) ? el : null;
                });
                ScrollIntoView(element);
                return element;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find element: {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Reusable: Find multiple web elements with wait
        protected IReadOnlyCollection<IWebElement> FindWebElements(By locator)
        {
            try
            {
                return wait.Until(drv =>
                {
                    var elements = drv.FindElements(locator);
                    return (elements != null && elements.Any(e => e.Displayed)) ? elements : null;
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to find elements: {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Scroll element into view
        private void ScrollIntoView(IWebElement element)
        {
            try
            {
                if (element != null)
                {
                    ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center', inline: 'nearest'});", element);
                }
            }
            catch
            {
                // Ignore if JS not supported
            }
        }

        // 🔹 Click element safely
        protected void Click(By locator)
        {
            try
            {
                var element = FindWebElement(locator);
                element.Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to click element: {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Send text safely
        protected void SendText(By locator, string text)
        {
            try
            {
                var element = FindWebElement(locator);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send text to element: {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Select dropdown by visible text
        protected void SelectDropdownByText(By locator, string visibleText)
        {
            try
            {
                var element = FindWebElement(locator);
                var selectElement = new SelectElement(element);
                selectElement.SelectByText(visibleText);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select dropdown option '{visibleText}' from {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Select dropdown by value
        protected void SelectDropdownByValue(By locator, string value)
        {
            try
            {
                var element = FindWebElement(locator);
                var selectElement = new SelectElement(element);
                selectElement.SelectByValue(value);
            }
            catch (Exception ex)
            {
               throw new Exception($"Failed to select dropdown value '{value}' from {locator}. Error: {ex.Message}");
            }
        }

        // 🔹 Get text from element
        protected string GetText(By locator)
        {
            try
            {
                var element = FindWebElement(locator);
                return element.Text;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get text from element: {locator}. Error: {ex.Message}");
            }
        }
    }
}