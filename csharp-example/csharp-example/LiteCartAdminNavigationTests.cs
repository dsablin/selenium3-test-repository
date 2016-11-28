using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartAdminNavigationTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminNavigationTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/");

            var menuList = Driver.FindElements(By.CssSelector("#box-apps-menu li"));
            for (var index = 0; index < menuList.Count; index++)
            {
                Driver.FindElements(By.CssSelector("#box-apps-menu>li"))[index].Click();
                WaitPageHeaderLoaded();

                var subMenuList = Driver.FindElements(By.CssSelector("#box-apps-menu>li .docs>li"));
                if (subMenuList.Count <= 0) continue;
                for (var i = 0; i < subMenuList.Count; i++)
                {
                    var subMmenuItem = Driver.FindElements(By.CssSelector("#box-apps-menu li .docs>li"))[i];
                    if (subMmenuItem.GetAttribute("class").Contains("selected")) continue;

                    subMmenuItem.Click();
                    WaitPageHeaderLoaded();
                }
            }
            Driver.FindElement(By.ClassName("fa-sign-out")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
        }

        private void WaitPageHeaderLoaded()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content>h1")));
        }
    }
}
