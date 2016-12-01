
using csharp_example.Helpers;
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
            var email = RandomUtils.GetRandomString(10) + "@gm.com";
            var password = RandomUtils.GetRandomString(10);

            RunLiteCartOnlineStore();

            Driver.FindElement(By.CssSelector("form[name=login_form] a")).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#create-account>h1"), "Create Account"));
            
            Input(Driver.FindElement(By.Name("firstname"))).SetText(RandomUtils.GetRandomString(7) + "_userFN");
            Input(Driver.FindElement(By.Name("lastname"))).SetText(RandomUtils.GetRandomString(7) + "UserLN");
            Input(Driver.FindElement(By.Name("address1"))).SetText(RandomUtils.GetRandomString(7) + "_address1");
            Input(Driver.FindElement(By.Name("postcode"))).SetText(RandomUtils.GenerateNumberWithLength(6));
            Input(Driver.FindElement(By.Name("city"))).SetText(RandomUtils.GetRandomString(7) + "_city");
            Input(Driver.FindElement(By.Name("email"))).SetText(email);
            Input(Driver.FindElement(By.Name("phone"))).SetText("+7" + RandomUtils.GenerateNumberWithLength(7));
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
