using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
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
        public void Firsttest()
        {
            _driver.Url = "http://www.google.com/";
            _driver.FindElement(By.Name("q")).SendKeys("webdriver");
            _driver.FindElement(By.Name("btnG")).Click();
            _wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
