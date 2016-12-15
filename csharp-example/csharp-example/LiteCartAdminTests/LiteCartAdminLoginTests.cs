using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartAdminTests
{
    [TestFixture]
    public class LiteCartAdminLoginTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminLoginTest()
        {
            LoginToLiteCartAdminConsole("http://192.168.56.1/litecart/admin/");

            Driver.FindElement(By.ClassName("fa-sign-out")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
        }
    }
}
