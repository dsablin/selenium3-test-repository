using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartAdminNavigationTests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            //_driver = new EdgeDriver();
            //_driver = new ChromeDriver();
            //_driver = new InternetExplorerDriver();

            //FirefoxBinary binary = new FirefoxBinary(@"c:\Program Files (x86)\Mozilla Firefox 45 ESR\firefox.exe");
            //FirefoxProfile profile = new FirefoxProfile();
            //_driver = new FirefoxDriver(binary, profile);

            var options = new FirefoxOptions {
                UseLegacyImplementation = false,
                BrowserExecutableLocation = @"c:\Program Files (x86)\Nightly\firefox.exe"
                //BrowserExecutableLocation = @"c:\Program Files (x86)\Mozilla Firefox 45 ESR\firefox.exe";
            };
            _driver = new FirefoxDriver(options);

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
        [Test]
        public void LiteCartAdminLoginTest()
        {
            _driver.Url = "http://localhost/litecart/admin/";
            _driver.FindElement(By.Name("username")).SendKeys("admin");
            _driver.FindElement(By.Name("password")).SendKeys("admin");
            _driver.FindElement(By.Name("login")).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-apps-menu-wrapper")));
            _driver.FindElement(By.ClassName("fa-sign-out")).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
