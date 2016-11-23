using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartAdminLoginTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void LiteCartAdminNavigationTest()
        {
            _driver.Url = "http://localhost/litecart/admin/";
            _driver.FindElement(By.Name("username")).SendKeys("admin");
            _driver.FindElement(By.Name("password")).SendKeys("admin");
            _driver.FindElement(By.Name("login")).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-apps-menu-wrapper")));

            var menuList = _driver.FindElements(By.CssSelector("#box-apps-menu li"));
            for (var index = 0; index < menuList.Count; index++)
            {
                _driver.FindElements(By.CssSelector("#box-apps-menu>li"))[index].Click();
                WaitPageHeaderLoaded();

                var subMenuList = _driver.FindElements(By.CssSelector("#box-apps-menu>li .docs>li"));
                if (subMenuList.Count <= 0) continue;
                for (var i = 0; i < subMenuList.Count; i++)
                {
                    var subMmenuItem = _driver.FindElements(By.CssSelector("#box-apps-menu li .docs>li"))[i];
                    if (subMmenuItem.GetAttribute("class").Contains("selected")) continue;

                    subMmenuItem.Click();
                    WaitPageHeaderLoaded();
                }
            }
            _driver.FindElement(By.ClassName("fa-sign-out")).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
        }

        private void WaitPageHeaderLoaded()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content>h1")));
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
