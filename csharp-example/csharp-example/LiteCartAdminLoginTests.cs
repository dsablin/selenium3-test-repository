﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class LiteCartAdminLoginTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminLoginTest()
        {
            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/");

            Driver.FindElement(By.ClassName("fa-sign-out")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Name("login")));
        }
    }
}
