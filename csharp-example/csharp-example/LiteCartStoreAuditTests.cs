using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartStoreAuditTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new FirefoxDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LiteCartStoreLabelsAuditTest()
        {
            _driver.Url = "http://localhost/litecart/en/";
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".middle>.content")));

            var productsList = _driver.FindElements(By.CssSelector(".product"));
            foreach (var product in productsList) {
                try {
                    Assert.IsTrue(product.FindElements(By.CssSelector(".sticker")).Count == 1);
                } catch (AssertionException) {
                    var prodName = product.FindElement(By.CssSelector("a.link")).GetAttribute("title");
                    var category = product.FindElement(By.XPath("ancestor::div/h3")).Text;
                    throw new Exception("The product '" + prodName + "' in the category '" + category + "' has more than one sticker.");
                }
            }
        }

        [Test]
        public void LiteCartStoreProductAuditTest()
        {
            const string category = "Campaigns";
            const int productIndex = 1;

            _driver.Url = "http://localhost/litecart/en/";
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".middle>.content")));

            var catProduct = _driver.FindElement(By.XPath(
                GetProductLinkXpath_ByCategoryAndIndex(category, productIndex)));

            var prodName = catProduct.FindElement(By.ClassName("name")).Text;
            var regularPrice = catProduct.FindElement(By.ClassName("regular-price")).Text;
            var campaignPrice = catProduct.FindElement(By.ClassName("campaign-price")).Text;
            VerifyProperPricesStyles(catProduct);

            catProduct.Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-product")));

            var productCard = _driver.FindElement(By.Id("box-product"));

            Assert.AreEqual(prodName, productCard.FindElement(By.ClassName("title")).Text);
            Assert.AreEqual(regularPrice, productCard.FindElement(By.ClassName("regular-price")).Text);
            Assert.AreEqual(campaignPrice, productCard.FindElement(By.ClassName("campaign-price")).Text);
            VerifyProperPricesStyles(productCard);
        }

        private static string GetProductLinkXpath_ByCategoryAndIndex(string category, int productIndex)
        {
            return $"//h3[contains(text(),'{category}')]/../div//li['{productIndex}']/a[@class='link']";
        }

        private static void VerifyProperPricesStyles(IWebElement element)
        {
            Assert.AreEqual("regular-price", element.FindElement(By.CssSelector("s")).GetAttribute("class"));
            Assert.AreEqual("campaign-price", element.FindElement(By.CssSelector("strong")).GetAttribute("class"));
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
