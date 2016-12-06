using System;
using csharp_example.Helpers;
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
            const string fileName = "jdphlogo.jpg";

            var productName = Guid.NewGuid().ToString("N").Substring(0, 10);
            var productCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            LoginToLiteCartAdminConsole("http://localhost/litecart/admin/");

            Driver.FindElement(By.CssSelector("#box-apps-menu a[href$=catalog]")).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Catalog"));
            Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".button[href$=edit_product]"))).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Add New Product"));

            OpenTab(tabGeneral);
            SetProductEnabled();
            FillInGeneralTabForm(productName, productCode, fileName);

            OpenTab(tabInformation);
            FillInInformationTabForm();

            OpenTab(tabData);
            FillInDataTabForm();

            Driver.FindElement(By.CssSelector("button[name=save]")).Click();
            Wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("#content>h1"), "Catalog"));
            Assert.True(Driver.FindElements(By.XPath($".//*[@id='content']//table//a[.='{productName}']")).Count == 1);
            Wait.Until(
                ExpectedConditions.ElementToBeClickable(By.XPath($".//*[@id='content']//table//a[.='{productName}']")));
            Driver.FindElement(By.XPath("//strong/a[.='[Root]']")).Click();
            Driver.FindElement(By.XPath("//td/a[.='Rubber Ducks']")).Click();
            Driver.FindElement(By.XPath("//td/a[.='Subcategory']")).Click();
            Assert.True(Driver.FindElements(By.XPath($".//*[@id='content']//table//a[.='{productName}']")).Count == 2);
        }

        #region subsidiary methods
        private void FillInGeneralTabForm(string productName, string productCode, string uploadFile)
        {
            PutValueByInputName("name[en]", productName);
            PutValueByInputName("code", productCode);
            Driver.FindElement(By.CssSelector("input[data-name=Subcategory]")).Click();
            GetDropdownOption_ByText_BySelectorName("default_category_id", "Subcategory");
            var genderList = Driver.FindElements(By.Name("product_groups[]"));
            var index = RandomUtils.GetRandomNumberFromInterval(genderList.Count);
            genderList[index].Click();
            PutValueByInputName("quantity", RandomUtils.GetRandomNumberStringFromInterval(100));
            GetDropdownOption_ByText_BySelectorName("sold_out_status_id", "Temporary sold out");
            var pathToUploadFile = GetPathToUploadFile(uploadFile);
            Driver.FindElement(By.CssSelector("input[type=file]")).SendKeys(pathToUploadFile);
            Driver.FindElement(By.Name("date_valid_from")).SendKeys("01.01.2016");
            Driver.FindElement(By.Name("date_valid_to")).SendKeys("01.01.2018");
        }

        private void FillInInformationTabForm()
        {
            GetDropdownOption_ByValue_BySelectorName("manufacturer_id", "1");
            PutValueByInputName("keywords", RandomUtils.GetRandomString(10) + "_Keyword");
            PutValueByInputName("short_description[en]", "TestSD_" + RandomUtils.GetRandomString(10));
            PutValueByInputName("description[en]", "TestSD_" + RandomUtils.GetRandomString(16));
            PutValueByInputName("head_title[en]", RandomUtils.GetRandomString(10) + "_Title");
            PutValueByInputName("meta_description[en]", "Test Meta Description: " + RandomUtils.GetRandomString(10));
        }

        private void FillInDataTabForm()
        {
            PutValueByInputName("sku", RandomUtils.GenerateNumberStringWithLength(7));
            PutValueByInputName("gtin", RandomUtils.GenerateNumberStringWithLength(12));
            PutValueByInputName("taric", RandomUtils.GenerateNumberStringWithLength(14));
            PutValueByInputName("weight", RandomUtils.GetRandomNumberStringFromInterval(999));
            GetDropdownOption_ByValue_BySelectorName("weight_class", "g");
            PutValueByInputName("dim_x", RandomUtils.GetRandomNumberStringFromInterval(100));
            PutValueByInputName("dim_y", RandomUtils.GetRandomNumberStringFromInterval(100));
            PutValueByInputName("dim_z", RandomUtils.GetRandomNumberStringFromInterval(100));
            GetDropdownOption_ByValue_BySelectorName("dim_class", "mm");
            PutValueByInputName("attributes[en]", RandomUtils.GetRandomString(16));
        }
        
        private static void GetDropdownOption_ByText_BySelectorName(string name, string text)
        {
            var selectDefaultCategory = new SelectElement(Driver.FindElement(By.Name(name)));
            selectDefaultCategory.SelectByText(text);
        }

        private void SetProductEnabled()
        {
            var enabledRb = Driver.FindElement(By.CssSelector("input[type=radio][value='1']"));
            if (enabledRb.GetAttribute("checked") != "true")
            {
                enabledRb.Click();
            }
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
        #endregion //subsidiary methods
    }
}
