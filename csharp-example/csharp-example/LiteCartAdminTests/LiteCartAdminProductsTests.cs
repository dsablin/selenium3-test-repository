using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartAdminTests
{
    [TestFixture]
    public class LiteCartAdminProductsTests : LiteCartAdiminBaseTestFixture
    {
        [Test]
        public void LiteCartAdminAddNewProductTest()
        {
            const string tabGeneral = "#tab-general";
            const string tabInformation = "#tab-information";
            const string tabData = "#tab-data";

            var productName = Guid.NewGuid().ToString("N").Substring(0, 10);
            var productCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/");

            Driver.FindElement(By.CssSelector("#box-apps-menu a[href$=catalog]")).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Catalog"));
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".button[href$=edit_product]"))).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Add New Product"));

            OpenTab(tabGeneral);
            //FillInGeneralTabForm();
            Input(Driver.FindElement(By.Name("name[en]"))).SetText(productName);
            Input(Driver.FindElement(By.Name("code"))).SetText(productCode);
            Driver.FindElement(By.CssSelector("input[data-name=Subcategory]")).Click();
            //def.category
            var select = Driver.FindElement(By.Name("default_category_id"));
            var selectDefaultCategory = new SelectElement(select);
            selectDefaultCategory.SelectByText("Subcategory");
            //gender
            var genderList = Driver.FindElements(By.Name("product_groups[]"));
            Random random = new Random();
            int index =  random.Next(1, genderList.Count);
            genderList[index].Click();
            Input(Driver.FindElement(By.Name("quantity"))).SetText("10");
            //Sold Out status
            var status = Driver.FindElement(By.Name("sold_out_status_id"));
            var soldOutStatus = new SelectElement(status);
            soldOutStatus.SelectByText("Temporary sold out");
            //upload file
            var relativeFilePath = String.Format(".\\TestData\\jdphlogo.jpg");
            var absoluteFilePath = Path.Combine(Environment.CurrentDirectory, relativeFilePath);

            Driver.FindElement(By.Name("new_images[]")).SendKeys(Path.GetFullPath(absoluteFilePath));
            Driver.FindElement(By.Name("date_valid_from")).SendKeys("01.01.2016");
            Driver.FindElement(By.Name("date_valid_to")).SendKeys("01.01.2018");


            //
            OpenTab(tabInformation);
            //
            OpenTab(tabData);

        }

        private void OpenTab(string tabname)
        {
            var tab = Driver.FindElement(By.CssSelector($"#content li>a[href*='{tabname}']"));

            var name = tab.FindElement(By.XPath("ancestor::li")).Text;
            var state = tab.FindElement(By.XPath("ancestor::li")).GetAttribute("class");
            if ("active".Equals(state)) return;
            tab.Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElement(Driver.FindElement(By.CssSelector("form li.active")), name));
        }

        private void WaitPageHeaderLoaded()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content>h1")));
        }
    }
}
