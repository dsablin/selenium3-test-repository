using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartOnlineStoreTests
{
    [TestFixture]
    public class LiteCartStoreUsersTests : LiteCartStoreBaseTestFixture
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
    }
}
