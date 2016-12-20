using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;

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
           /* var options = new ChromeOptions();

            var proxy = new Proxy
            {
                Kind = ProxyKind.Manual,
                HttpProxy = "localhost:8888"
            };
            options.Proxy = proxy;
            */
            //options.SetLoggingPreference(LogType.Browser, LogLevel.All);

            Driver = new ChromeDriver();
            //Driver = new EdgeDriver();
            //Driver = new FirefoxDriver();
            //Driver = new InternetExplorerDriver();

            #region EventFiring
            //EventFiringWebDriver driver = new EventFiringWebDriver(new ChromeDriver());
            //driver.FindingElement += (sender, e) => Console.WriteLine(e.FindMethod);
            //driver.FindElementCompleted += (sender, e) => Console.WriteLine(e.FindMethod + " found");
            //driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
            //Driver = driver;
            #endregion //EventFiring

            #region Remote
            /*
            DesiredCapabilities capability = DesiredCapabilities.Chrome();
            capability.SetCapability("browserstack.user", "diversant1");
            capability.SetCapability("browserstack.key", "ezmwLyCdpLsC1HVj9Dbg");
            capability.SetCapability("build", "First build");
            capability.SetCapability("browserstack.debug", "true");
            capability.SetCapability("browserstack.local", "true");
            capability.SetCapability("browser", "IE");
            capability.SetCapability("browser_version", "11.0");
            capability.SetCapability("os", "Windows");
            capability.SetCapability("os_version", "8.1");

            Driver = new RemoteWebDriver(
              new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability);

            //Driver = new RemoteWebDriver(new Uri("http://10.110.4.23:4444/wd/hub"), DesiredCapabilities.InternetExplorer());
            //Driver = new RemoteWebDriver(new Uri("http://192.168.56.1:4444/wd/hub"), DesiredCapabilities.Chrome());
            //Driver = new RemoteWebDriver(new Uri("http://10.110.4.23:4444/wd/hub"), DesiredCapabilities.Firefox());
            */
            #endregion //Remote

            #region FF
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
            #endregion //FF

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
            var dllDirPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(BaseTestfixture)).Location);
            var absoluteFilePath = Path.Combine(dllDirPath, relativeFilePath);
            return Path.GetFullPath(absoluteFilePath);
        }


        public static void GetDropdownOption_ByValue_BySelectorName(string name, string value)
        {
            var selectDefaultCategory = new SelectElement(Driver.FindElement(By.Name(name)));
            selectDefaultCategory.SelectByValue(value);
        }


        [TearDown]
        public void Stop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}