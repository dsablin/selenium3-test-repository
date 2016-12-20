using System;
using System.Collections.Generic;
using csharp_example.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example.PagesObjects
{
    internal class StoreHomePage : BasePage
    {
        #region Map of Elements

        [FindsBy(How = How.CssSelector, Using = "#box-most-popular")]
        public IWebElement ElementMostPopularProductsSection;

        [FindsBy(How = How.CssSelector, Using = ".middle>.content")]
        private IWebElement ElementStoreHomePane;

        [FindsBy(How = How.CssSelector, Using = "#box-most-popular .product")]
        private IList<IWebElement> ElementMostPopularProductsList;

        [FindsBy(How = How.CssSelector, Using = "#cart .link")]
        private IWebElement ElementCheckOutLink;

        [FindsBy(How = How.CssSelector, Using = "#cart .quantity")]
        private IWebElement ElementCartGoodsCounter;

        #endregion

        public StoreHomePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        public int GetCurrentCartItemsCount()
        {
            return Convert.ToInt32(ElementCartGoodsCounter.Text);
        }

        public void ChooseAnyPopularProduct()
        {
            var ind = RandomUtils.GetRandomNumberFromInterval(ElementMostPopularProductsList.Count);
            ElementMostPopularProductsList[ind].Click();
        }

        public void OpenCart()
        {
            ElementCheckOutLink.Click();
        }

        public void WaitStorePageLoaded()
        {
            WaitElement(ElementStoreHomePane);
        }

    }
}
