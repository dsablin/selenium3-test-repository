using System;
using System.Collections.Generic;
using csharp_example.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.LiteCartOnlineStoreTests
{
    class LiteCartGoogsOrderTests : LiteCartStoreBaseTestFixture
    {
        [Test]
        public void LiteCartStoreManageCartTest()
        {
            RunLiteCartOnlineStore();
            var qInit = GetCurrentCartItemsCount();

            for (var i = 0; i < 3; i++)
            {
                int qNew;
                ChooseAnyPopularProduct();
                SelectSizeOptionIfAvailable();
                var elementAddTocartButton = AddProductToCart_CheckCartCounter(qInit, out qNew);
                ReturnToHomePage(elementAddTocartButton);
                Assert.AreEqual(qNew, GetCurrentCartItemsCount());
                qInit = qNew;
            }

            Driver.FindElement(By.CssSelector("#cart .link")).Click();
            int count;
            Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("box-checkout-cart")));
            var cartRange = Driver.FindElements(By.CssSelector(".shortcuts li")).Count;
            if (cartRange > 0) {
                for (var i = 0; i < cartRange; i++)
                {
                    count = GetProductsCountFromTable();
                    var currentList = Driver.FindElements(By.CssSelector(".shortcuts li"));
                    if (currentList.Count != 0) {
                        currentList[0].Click();
                    }
                    RemoveProductFromCart_CheckTableRowsChanges(count);
                }
            } else {
                count = GetProductsCountFromTable();
                RemoveProductFromCart_CheckTableRowsChanges(count);
            }
            Driver.FindElement(By.CssSelector("#checkout-cart-wrapper a")).Click();
            Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".middle>.content")));
        }

        #region subsidiary methods

        private static void RemoveProductFromCart_CheckTableRowsChanges(int count)
        {
            Driver.FindElements(By.CssSelector("button[name=remove_cart_item]"))[0].Click();
            count = count - 1;
            Wait.Until(d =>
            {
                IList<IWebElement> l = d.FindElements(By.CssSelector("#order_confirmation-wrapper tr td.item"));
                return l.Count == (count) ? l : null;
            });
        }

        private static int GetProductsCountFromTable()
        {
            var count = Driver.FindElements(By.CssSelector("#order_confirmation-wrapper tr td.item")).Count;
            return count;
        }

        private static void SelectSizeOptionIfAvailable()
        {
            if (Driver.FindElements(By.Name("options[Size]")).Count <= 0) return;
            var selectDefaultCategory = new SelectElement(Driver.FindElement(By.Name("options[Size]")));
            var options = selectDefaultCategory.AllSelectedOptions.Count;
            selectDefaultCategory.SelectByIndex(RandomUtils.GetRandomNumberFromInterval(options));
        }

        private static IWebElement AddProductToCart_CheckCartCounter(int qInit, out int qNew)
        {
            var elementAddTocartButton = Driver.FindElement(By.Name("add_cart_product"));
            elementAddTocartButton.Click();
            qNew = qInit + 1;
            Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[@id='cart']//*[@class='quantity'][.='{qNew}']")));
            return elementAddTocartButton;
        }

        private static int GetCurrentCartItemsCount()
        {
            return Convert.ToInt32(Driver.FindElement(By.CssSelector("#cart .quantity")).Text);
        }

        private static void ChooseAnyPopularProduct()
        {
            var popularProductsList = Driver.FindElements(By.CssSelector("#box-most-popular .product"));
            var prodcount = popularProductsList.Count;
            var ind = RandomUtils.GetRandomNumberFromInterval(prodcount);
            popularProductsList[ind].Click();
        }

        private static void ReturnToHomePage(IWebElement elementAddTocartButton)
        {
            Driver.FindElement(By.XPath("//*[@id='main-wrapper']//*[@id='breadcrumbs']//a[.='Home']")).Click();
            Wait.Until(ExpectedConditions.StalenessOf(elementAddTocartButton));
        }

        #endregion //subsidiary methods
    }
}
