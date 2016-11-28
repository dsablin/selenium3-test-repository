﻿using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartAdminAuditTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminCountriesSortingTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/?app=countries&doc=countries");
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Countries"));

            var cNames = Driver.FindElements(By.CssSelector(".row>td>a:not([title])")).Select(country => country.Text).ToList();

            var cNamesSorted = GetSortedListModel(cNames);
            Assert.IsTrue(cNames.SequenceEqual(cNamesSorted), "The Countries' list is not sorted alphabetically.");

            for (var index = 0; index < cNames.Count; index++)
            {
                var countryZones = Driver.FindElements(By.CssSelector("#content .row td:nth-of-type(6)"))[index];

                if (Convert.ToInt32(countryZones.GetAttribute("textContent")) <= 0) continue;

                countryZones.FindElement(By.XPath("../td[7]/a")).Click();
                Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Edit Country"));

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
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Geo Zones"));

            var cNames = Driver.FindElements(By.CssSelector("form[name=geo_zones_form] .row"));

            for (var index = 0; index < cNames.Count; index++)
            {
                Driver.FindElements(By.CssSelector(".row>td>a:not([title])"))[index].Click();
                Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Edit Geo Zone"));

                var zonesList = Driver.FindElements(By.CssSelector("#table-zones td:nth-of-type(3) option[selected]"));
                var zNames = zonesList.Select(zName => zName.GetAttribute("innerText")).Where(name => name != "").ToList();

                var zNamesSorted = GetSortedListModel(zNames);
                Assert.IsTrue(zNames.SequenceEqual(zNamesSorted), "The Time Zones' list is not sorted alphabetically.");

                Driver.FindElement(By.Name("cancel")).Click();
            }
        }

        private static IEnumerable<string> GetSortedListModel(IEnumerable<string> cNames)
        {
            var listSorted = cNames.Select(d => d).ToList();
            listSorted.Sort();
            return listSorted;
        }
    }
}