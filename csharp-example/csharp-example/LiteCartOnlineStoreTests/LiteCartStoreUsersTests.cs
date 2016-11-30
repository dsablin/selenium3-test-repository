using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartOnlineStoreTests
{
    [TestFixture]
    public class LiteCartStoreUsersTests : LiteCartStoreBaseTestFixture
    {
        [Test]
        public void LiteCartStoreAddNewUserTest()
        {
            var email = Guid.NewGuid().ToString("N").Substring(0, 10) + "@gm.com";
            var password = Guid.NewGuid().ToString("N").Substring(0, 10);

            RunLiteCartOnlineStore();

            Driver.FindElement(By.CssSelector("form[name=login_form] a")).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#create-account>h1"), "Create Account"));
            
            Input(Driver.FindElement(By.Name("firstname"))).SetText("UserFirstName");
            Input(Driver.FindElement(By.Name("lastname"))).SetText("UserLastName");
            Input(Driver.FindElement(By.Name("address1"))).SetText("UserAddress1");
            Input(Driver.FindElement(By.Name("postcode"))).SetText("197000");
            Input(Driver.FindElement(By.Name("city"))).SetText("UserCity");
            Input(Driver.FindElement(By.Name("email"))).SetText(email);
            Input(Driver.FindElement(By.Name("phone"))).SetText("+73232323");
            Input(Driver.FindElement(By.Name("password"))).SetText(password);
            Input(Driver.FindElement(By.Name("confirmed_password"))).SetText(password);

            Driver.FindElement(By.Name("create_account")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navigation")));

            CustomerLogout();

            Input(Driver.FindElement(By.Name("email"))).SetText(email);
            Input(Driver.FindElement(By.Name("password"))).SetText(password);
            Driver.FindElement(By.Name("login")).Click();

            CustomerLogout();
        }

        private void CustomerLogout()
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".list-vertical a[href$=logout]"))).Click();
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("login")));
        }
    }
}
