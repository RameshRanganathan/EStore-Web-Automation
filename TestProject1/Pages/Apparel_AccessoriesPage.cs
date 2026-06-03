using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using Test_Store_Automation.Utils;

namespace Test_Store_Automation.Pages
{
    internal class Apparel_AccessoriesPage : PageBase
    {
        // Locators for Hair Care category page
        private readonly By productNames = By.CssSelector(".fixed .prdocutname");
        private readonly By productPrices = By.CssSelector(".oneprice, .pricenew");
        private readonly By addToCartButtons = By.CssSelector(".productcart");
        private readonly By successAlert = By.CssSelector(".alert-success");
        private readonly By viewCartButton = By.CssSelector("a[title='View Cart']");
        private readonly By sortDropdown = By.CssSelector("select[name='sort']"); // optional sorting dropdown

        public Apparel_AccessoriesPage(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Navigate directly to Hair Care category page.
        /// </summary>
        public void GoTo()
        {
            driver.Navigate().GoToUrl("https://automationteststore.com/index.php?rt=product/category&path=68");
        }

        /// <summary>
        /// Get all product names listed on the Hair Care page.
        /// </summary>
        public IList<string> GetProductNames()
        {
            var elements = driver.FindElements(productNames);
            return elements.Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Get all product prices listed on the Hair Care page.
        /// </summary>
        public IList<string> GetProductPrices()
        {
            var elements = driver.FindElements(productPrices);
            return elements.Select(e => e.Text).ToList();
        }

        /// <summary>
        /// Click a product by its exact name.
        /// </summary>
        public void ClickProductByName(string productName)
        {
            var products = driver.FindElements(productNames);
            var product = products.FirstOrDefault(e => e.Text.Trim().Equals(productName, System.StringComparison.OrdinalIgnoreCase));
            if (product != null)
                product.Click();
            else
                throw new NoSuchElementException($"Product with name '{productName}' not found.");
        }

        /// <summary>
        /// Add the first product in the list to the cart.
        /// </summary>
        public void AddFirstProductToCart()
        {
            var buttons = driver.FindElements(addToCartButtons);
            if (buttons.Any())
                buttons.First().Click();
            else
                throw new NoSuchElementException("No Add to Cart buttons found.");
        }

        /// <summary>
        /// Add product to cart by index.
        /// </summary>
        public void AddProductToCartByIndex(int index)
        {
            var buttons = driver.FindElements(addToCartButtons);
            if (index >= 0 && index < buttons.Count)
                buttons[index].Click();
            else
                throw new NoSuchElementException($"No Add to Cart button at index {index}.");
        }

        /// <summary>
        /// Get success alert text after adding product to cart.
        /// </summary>
        public string GetSuccessAlert() => GetText(successAlert);

        /// <summary>
        /// Navigate to the cart page.
        /// </summary>
        public void ViewCart() => Click(viewCartButton);

        /// <summary>
        /// Sort products using the dropdown (if available).
        /// </summary>
        public void SortProducts(string sortOption) => SelectDropdownByText(sortDropdown, sortOption);
    }
}
