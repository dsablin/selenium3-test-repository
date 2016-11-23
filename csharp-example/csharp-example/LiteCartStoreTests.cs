using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartStoreTests
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
        public void LiteCartStoreAuditTest()
        {
            _driver.Url = "http://localhost/litecart/en/";
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".middle>.content")));

            var productsList = _driver.FindElements(By.CssSelector(".product"));
            foreach (var product in productsList)
            {
                try
                {
                    Assert.IsTrue(product.FindElements(By.CssSelector(".sticker")).Count == 1);
                }
                catch (AssertionException)
                {
                    var prodName = product.FindElement(By.CssSelector("a.link")).GetAttribute("title");
                    var category = product.FindElement(By.XPath("ancestor::div/h3")).Text;
                    throw new Exception("The product '" + prodName + "' in the category '" + category + "' has more than one sticker.");
                }
            }
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
