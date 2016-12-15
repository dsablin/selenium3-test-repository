using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartAdminTests
{
    [TestFixture]
    public class LiteCartAdminAuditTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminBrowserLoggingTest()
        {
            LoginToLiteCartAdminConsole();
            Driver.Url = "http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1";

            //foreach (var l in Driver.Manage().Logs.AvailableLogTypes) {
            //    Console.WriteLine(l);
            //}

            WaitPageHeaderLoaded("Catalog");
            var productsList = Driver.FindElements(By.CssSelector(".row>td [name*=products]"));

            for (var index = 0; index < productsList.Count; index++)
            {
                Driver.FindElements(
                    By.XPath(
                        "//*[@id='content']//table//td/*[contains(@name, 'products')]/../../td/a[not(contains(@title, 'Edit'))]"))
                        [index].Click();
                WaitPageHeaderLoaded("Edit Product");

                foreach (var l in Driver.Manage().Logs.GetLog("browser")) {
                    Console.WriteLine(l);
                }

                Driver.FindElement(By.Name("cancel")).Click();
                WaitPageHeaderLoaded("Catalog");
            }

        }

        [Test]
        public void LiteCartAdminCountryManageTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/?app=countries&doc=countries");
            WaitPageHeaderLoaded("Countries");
            Driver.FindElement(By.CssSelector("#content .button")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1[contains(text(), 'Add New Country')]")));
            var mainWindow = Driver.CurrentWindowHandle;

            var externalLinks = Driver.FindElements(By.CssSelector(".fa.fa-external-link"));
            foreach (var link in externalLinks)
            {
                ICollection<string> oldWindows = new List<string>(Driver.WindowHandles);
                link.Click();
                var newWindow = Wait.Until(ThereIsWindowOtherThan(oldWindows));
                if (newWindow == null) continue;
                Driver.SwitchTo().Window(newWindow);
                Driver.Close();
                Driver.SwitchTo().Window(mainWindow);
            }
        }

        [Test]
        public void LiteCartAdminCountriesSortingTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/?app=countries&doc=countries");
            WaitPageHeaderLoaded("Countries");

            var cNames = Driver.FindElements(By.CssSelector(".row>td>a:not([title])")).Select(country => country.Text).ToList();

            var cNamesSorted = GetSortedListModel(cNames);
            Assert.IsTrue(cNames.SequenceEqual(cNamesSorted), "The Countries' list is not sorted alphabetically.");

            for (var index = 0; index < cNames.Count; index++)
            {
                var countryZones = Driver.FindElements(By.CssSelector("#content .row td:nth-of-type(6)"))[index];

                if (Convert.ToInt32(countryZones.GetAttribute("textContent")) <= 0) continue;

                countryZones.FindElement(By.XPath("../td[7]/a")).Click();
                WaitPageHeaderLoaded("Edit Country");

                var zonesList = Driver.FindElements(By.CssSelector("#table-zones tr>td:nth-of-type(3)"));
                var zNames = zonesList.Select(zName => zName.GetAttribute("innerText")).Where(name => name != "").ToList();

                var zNamesSorted = GetSortedListModel(zNames);
                Assert.IsTrue(zNames.SequenceEqual(zNamesSorted), "The Time Zones' list is not sorted alphabetically.");

                Driver.FindElement(By.Name("cancel")).Click();
            }
        }

        [Test]
        public void LiteCartAdminGeoZonesSortingTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            WaitPageHeaderLoaded("Geo Zones");

            var cNames = Driver.FindElements(By.CssSelector("form[name=geo_zones_form] .row"));

            for (var index = 0; index < cNames.Count; index++)
            {
                Driver.FindElements(By.CssSelector(".row>td>a:not([title])"))[index].Click();
                WaitPageHeaderLoaded("Edit Geo Zone");

                var zonesList = Driver.FindElements(By.CssSelector("#table-zones td:nth-of-type(3) option[selected]"));
                var zNames = zonesList.Select(zName => zName.GetAttribute("innerText")).Where(name => name != "").ToList();

                var zNamesSorted = GetSortedListModel(zNames);
                Assert.IsTrue(zNames.SequenceEqual(zNamesSorted), "The Time Zones' list is not sorted alphabetically.");

                Driver.FindElement(By.Name("cancel")).Click();
            }
        }

        #region subsidiary methods

        public Func<IWebDriver, string> ThereIsWindowOtherThan(ICollection<string> oldWindows)
        {
            Func<IWebDriver, string> action = driver =>
            {
                ICollection<string> newWindows = driver.WindowHandles;
                return newWindows.FirstOrDefault(x => !oldWindows.Contains(x));
            };
            return action;
        }

        private static IEnumerable<string> GetSortedListModel(IEnumerable<string> cNames)
        {
            var listSorted = cNames.Select(d => d).ToList();
            listSorted.Sort();
            return listSorted;
        }

        #endregion //subsidiary methods
    }
}
