using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartOnlineStoreTests
{
    [TestFixture]
    public class LiteCartStoreAuditTests : LiteCartStoreBaseTestFixture
    {

        [Test]
        public void LiteCartStoreLabelsAuditTest()
        {
            RunLiteCartOnlineStore();

            var productsList = Driver.FindElements(By.CssSelector(".product"));
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

            RunLiteCartOnlineStore();

            var catProduct = Driver.FindElement(By.XPath(
                GetProductLinkXpath_ByCategoryAndIndex(category, productIndex)));

            var prodName = catProduct.FindElement(By.ClassName("name")).Text;
            var regularPrice = catProduct.FindElement(By.ClassName("regular-price")).Text;
            var campaignPrice = catProduct.FindElement(By.ClassName("campaign-price")).Text;
            VerifyProperPricesStyles(catProduct);

            catProduct.Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-product")));

            var productCard = Driver.FindElement(By.Id("box-product"));

            Assert.AreEqual(prodName, productCard.FindElement(By.ClassName("title")).Text);
            Assert.AreEqual(regularPrice, productCard.FindElement(By.ClassName("regular-price")).Text);
            Assert.AreEqual(campaignPrice, productCard.FindElement(By.ClassName("campaign-price")).Text);
            VerifyProperPricesStyles(productCard);
        }

        #region subsidiary methods

        private static string GetProductLinkXpath_ByCategoryAndIndex(string category, int productIndex)
        {
            return $"//h3[contains(text(),'{category}')]/../div//li['{productIndex}']/a[@class='link']";
        }

        private static void VerifyProperPricesStyles(ISearchContext element)
        {
            Assert.AreEqual("regular-price", element.FindElement(By.CssSelector("s")).GetAttribute("class"));
            Assert.AreEqual("campaign-price", element.FindElement(By.CssSelector("strong")).GetAttribute("class"));
        }

        #endregion //subsidiary methods
    }
}
