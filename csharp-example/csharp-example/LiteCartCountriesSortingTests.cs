using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartCountriesSortingTests
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
        public void LiteCartCountriesSortingTest()
        {
            _driver.Url = "http://localhost/litecart/admin/?app=countries&doc=countries";
            _driver.FindElement(By.Name("username")).SendKeys("admin");
            _driver.FindElement(By.Name("password")).SendKeys("admin");
            _driver.FindElement(By.Name("login")).Click();
            _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Countries"));

            var cNames = _driver.FindElements(By.CssSelector(".row>td>a:not([title])")).Select(country => country.Text).ToList();

            var cNamesSorted = GetSortedListModel(cNames);
            Assert.IsTrue(cNames.SequenceEqual(cNamesSorted), "The Countries' list is not sorted alphabetically.");

            for (var index = 0; index < cNames.Count; index++)
            {
                var countryZones = _driver.FindElements(By.CssSelector("#content .row td:nth-of-type(6)"))[index];

                if (Convert.ToInt32(countryZones.GetAttribute("textContent")) <= 0) continue;

                countryZones.FindElement(By.XPath("../td[7]/a")).Click();
                _wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Edit Country"));

                var zonesList = _driver.FindElements(By.CssSelector("#table-zones tr>td:nth-of-type(3)"));
                var zNames = zonesList.Select(zName => zName.GetAttribute("innerText")).Where(name => name != "").ToList();

                var zNamesSorted = GetSortedListModel(zNames);
                Assert.IsTrue(zNames.SequenceEqual(zNamesSorted), "The Time Zones' list is not sorted alphabetically.");

                _driver.FindElement(By.Name("cancel")).Click();
            }
        }

        private static IEnumerable<string> GetSortedListModel(IEnumerable<string> cNames)
        {
            var listSorted = cNames.Select(d => d).ToList();
            listSorted.Sort();
            return listSorted;
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
