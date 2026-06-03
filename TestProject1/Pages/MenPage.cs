using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Test_Store_Automation.Utils;

namespace Test_Store_Automation.Pages
{
    internal class MenPage : PageBase
    {
        private readonly By productNames = By.CssSelector(".fixed .prdocutname");
        private readonly By productPrices = By.CssSelector(".oneprice, .pricenew");
        private readonly By addToCartButtons = By.CssSelector(".productcart");
        private readonly By successAlert = By.CssSelector(".alert-success");
        private readonly By viewCartButton = By.CssSelector("a[title='View Cart']");
        private readonly By sortDropdown = By.CssSelector("select[name='sort']");

        public MenPage(IWebDriver driver) : base(driver) { }

        public void GoTo()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=product/category&path=58");
        }

        public IList<string> GetProductNames() =>
            driver.FindElements(productNames).Select(e => e.Text).ToList();

        public IList<string> GetProductPrices() =>
            driver.FindElements(productPrices).Select(e => e.Text).ToList();

        public void ClickProductByName(string productName)
        {
            var products = driver.FindElements(productNames);
            var product = products.FirstOrDefault(e => e.Text.Trim().Equals(productName, System.StringComparison.OrdinalIgnoreCase));
            if (product != null) product.Click();
            else throw new NoSuchElementException($"Product '{productName}' not found.");
        }

        public void AddFirstProductToCart()
        {
            var buttons = driver.FindElements(addToCartButtons);
            if (buttons.Any()) buttons.First().Click();
            else throw new NoSuchElementException("No Add to Cart buttons found.");
        }

        public void AddProductToCartByIndex(int index)
        {
            var buttons = driver.FindElements(addToCartButtons);
            if (index >= 0 && index < buttons.Count) buttons[index].Click();
            else throw new NoSuchElementException($"No Add to Cart button at index {index}.");
        }

        public string GetSuccessAlert() => GetText(successAlert);

        public void ViewCart() => Click(viewCartButton);

        public void SortProducts(string sortOption) => SelectDropdownByText(sortDropdown, sortOption);
    }
}
