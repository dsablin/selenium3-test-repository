using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        public void WaitElement(IWebElement element)
        {
            Wait.Until(
                driver => {
                    try {
                        return element.Displayed;
                    } catch (NoSuchElementException) {
                        return false;
                    }
                });
        }

        public bool IsControlAvailable(IWebElement webElement)
        {
            try
            {
                return webElement.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
