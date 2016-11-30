using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartAdminTests
{
    public class LiteCartAdiminBaseTestFixture : BaseTestfixture
    {
        public void LoginToLiteCartAdminConsole(string url)
        {
            Driver.Url = url;
            Driver.FindElement(By.Name("username")).SendKeys("admin");
            Driver.FindElement(By.Name("password")).SendKeys("admin");
            Driver.FindElement(By.Name("login")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-apps-menu-wrapper")));
        }
    }
}