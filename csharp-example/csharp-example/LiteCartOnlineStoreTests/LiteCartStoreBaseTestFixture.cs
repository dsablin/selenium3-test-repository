using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartOnlineStoreTests
{
    public class LiteCartStoreBaseTestFixture : BaseTestfixture
    {
        public void RunLiteCartOnlineStore()
        {
            Driver.Url = "http://localhost/litecart/en/";
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".middle>.content")));
        }
    }
}