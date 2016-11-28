using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace csharp_example
{
    public class LiteCartAdiminBaseTestFixture
    {
        public IWebDriver Driver;
        public WebDriverWait Wait;

        [SetUp]
        public void Start()
        {
            Driver = new ChromeDriver();
            //Driver = new EdgeDriver();
            //Driver = new ChromeDriver();
            //Driver = new InternetExplorerDriver();

            //FirefoxBinary binary = new FirefoxBinary(@"c:\Program Files (x86)\Mozilla Firefox 45 ESR\firefox.exe");
            //FirefoxProfile profile = new FirefoxProfile();
            //Driver = new FirefoxDriver(binary, profile);

            //var options = new FirefoxOptions
            //{
            //    UseLegacyImplementation = false,
            //    BrowserExecutableLocation = @"c:\Program Files (x86)\Nightly\firefox.exe"
            //    //BrowserExecutableLocation = @"c:\Program Files (x86)\Mozilla Firefox 45 ESR\firefox.exe";
            //};
            //Driver = new FirefoxDriver(options);

            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));


        }

        public void LoginToLiteCartAdminConsole(string url)
        {
            Driver.Url = url;
            Driver.FindElement(By.Name("username")).SendKeys("admin");
            Driver.FindElement(By.Name("password")).SendKeys("admin");
            Driver.FindElement(By.Name("login")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-apps-menu-wrapper")));
        }

        [TearDown]
        public void Stop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}