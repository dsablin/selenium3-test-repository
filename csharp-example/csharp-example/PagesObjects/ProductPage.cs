using Microsoft.VisualStudio.TestTools.UnitTesting;
using csharp_example.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace csharp_example.PagesObjects
{
    internal class ProductPage : BasePage
    {
        #region Map of Elements

        [FindsBy(How = How.Name, Using = "add_cart_product")]
        private IWebElement ElementAddToCartButton;

        [FindsBy(How = How.Name, Using = "options[Size]")]
        private IWebElement ElementSizeSelector;

        [FindsBy(How = How.XPath, Using = "//*[@id='main-wrapper']//*[@id='breadcrumbs']//a[.='Home']")]
        private IWebElement ElementHomePageLink;

        #endregion

        public ProductPage(IWebDriver driver) : base(driver){
            PageFactory.InitElements(driver, this);
        }

        public StoreHomePage ReturnToHomePage()
        {
            ElementHomePageLink.Click();
            var storeHome = new StoreHomePage(Driver);
            WaitElement(storeHome.ElementMostPopularProductsSection);
            return storeHome;
        }

        public void SelectAnySizeOptionIfAvailable() {
            if (!IsControlAvailable(ElementSizeSelector)) return;
            var sizeSelector = new SelectElement(ElementSizeSelector);
            sizeSelector.SelectByIndex(
                RandomUtils.GetRandomNumberFromInterval(sizeSelector.AllSelectedOptions.Count));
        }

        public void MoveProductsToCartByNumberGiven(StoreHomePage storeHome, int qnty, int countInitial)
        {
            for (var i = 0; i < qnty; i++)
            {
                
                storeHome.ChooseAnyPopularProduct();
                SelectAnySizeOptionIfAvailable();

                ElementAddToCartButton.Click();
                var countNew = countInitial + 1;
                Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[@id='cart']//*[@class='quantity'][.='{countNew}']")));

                storeHome = ReturnToHomePage();
                Assert.AreEqual(countNew, storeHome.GetCurrentCartItemsCount());
                countInitial = countNew;
            }
        }
    }
}
