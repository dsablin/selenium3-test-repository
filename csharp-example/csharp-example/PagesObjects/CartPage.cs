using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace csharp_example.PagesObjects
{
    internal class CartPage : BasePage
    {
        #region Map of Elements

        [FindsBy(How = How.CssSelector, Using = "#order_confirmation-wrapper tr td.item")]
        private IList<IWebElement> ElementCartGridProductsList;

        [FindsBy(How = How.CssSelector, Using = "button[name=remove_cart_item]")]
        private IList<IWebElement> ElementCartProductsRemoveButtonsList;

        [FindsBy(How = How.CssSelector, Using = ".shortcuts li")]
        private IList<IWebElement> ElementCartGoodsLabelsList;

        [FindsBy(How = How.CssSelector, Using = "#checkout-cart-wrapper a")]
        private IWebElement ElementCartBackLink;

        [FindsBy(How = How.Id, Using = "box-checkout-cart")]
        private IWebElement ElementCartCheckoutBox;

        #endregion

        public CartPage(IWebDriver driver) : base(driver) {
            PageFactory.InitElements(driver, this);
        }


        public void ClearCart()
        {
            //work with cart page
            int count;
            WaitElement(ElementCartCheckoutBox);
            var cartRange = ElementCartGoodsLabelsList.Count;
            if (cartRange > 0)
            {
                for (var i = 0; i < cartRange; i++)
                {
                    count = GetProductsCountFromTable();
                    if (ElementCartGoodsLabelsList.Count != 0)
                    {
                        ElementCartGoodsLabelsList[0].Click();
                    }
                    RemoveProductFromCart_CheckTableRowsChanges(count);
                }
            }
            else
            {
                count = GetProductsCountFromTable();
                RemoveProductFromCart_CheckTableRowsChanges(count);
            }
            ElementCartBackLink.Click();
        }

        public void RemoveProductFromCart_CheckTableRowsChanges(int count)
        {
            ElementCartProductsRemoveButtonsList[0].Click();
            count = count - 1;
            Wait.Until(d => ElementCartGridProductsList.Count == (count));
        }

        public int GetProductsCountFromTable()
        {
            return ElementCartGridProductsList.Count;
        }
    }
}
