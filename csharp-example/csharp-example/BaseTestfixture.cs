using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace csharp_example
{
    public class BaseTestfixture
    {
        public static IWebDriver Driver;
        public static WebDriverWait Wait;
        const string UploadDir = ".\\TestData\\";

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

        public static InputHelpers Input(IWebElement webElement)
        {
            return new InputHelpers(webElement);
        }

        public class InputHelpers
        {
            private readonly IWebElement _webElement;

            public InputHelpers(IWebElement webElement)
            {
                _webElement = webElement;
            }

            public void SetText(string text)
            {
                _webElement.Clear();
                _webElement.SendKeys(text);
            }
        }

        public void PutValueByInputName(string inputName, string text)
        {
            Input(Driver.FindElement(By.Name(inputName))).SetText(text);
        }

        public static string GetPathToUploadFile(string fileName)
        {
            var relativeFilePath = string.Format(UploadDir + fileName);
            var dllDirPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(MyFirstTest)).Location);
            var absoluteFilePath = Path.Combine(dllDirPath, relativeFilePath);
            return Path.GetFullPath(absoluteFilePath);
        }

        [TearDown]
        public void Stop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}