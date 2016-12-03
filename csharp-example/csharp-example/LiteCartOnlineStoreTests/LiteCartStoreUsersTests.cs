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

            PutValueByInputName("firstname", RandomUtils.GetRandomString(7) + "_userFN");
            PutValueByInputName("lastname", RandomUtils.GetRandomString(7) + "UserLN");
            PutValueByInputName("address1", RandomUtils.GetRandomString(7) + "_address1");
            PutValueByInputName("postcode", RandomUtils.GenerateNumberStringWithLength(6));
            PutValueByInputName("city", RandomUtils.GetRandomString(7) + "_city");
            PutValueByInputName("email", email);
            PutValueByInputName("phone", "+7" + RandomUtils.GenerateNumberStringWithLength(7));
            PutValueByInputName("password", password);
            PutValueByInputName("confirmed_password", password);

            Driver.FindElement(By.Name("create_account")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("navigation")));

            CustomerLogout();

            PutValueByInputName("email", email);
            PutValueByInputName("password", password);
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
