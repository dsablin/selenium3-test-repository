using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
        //[Test]
        public void Firsttest()
        {
            _driver.Url = "http://www.google.com/";
            _driver.FindElement(By.Name("q")).SendKeys("webdriver");
            _driver.FindElement(By.Name("btnG")).Click();
            _wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }
/*
        public class Product
        {
            public Product(string prodName, string regularPrice, string campaignPrice)
            {
                ProductName = prodName;
                RegularPrice = regularPrice;
                CampaignPrice = campaignPrice;
            }

            public string ProductName { get; set; }
            public string RegularPrice { get; set; }
            public string CampaignPrice { get; set; }

        }

        public class PermitsComparer : IEqualityComparer<PermitsList>
        {

            public bool Equals(PermitsList x, PermitsList y)
            {
                //Check whether the objects are the same object. 
                if (Object.ReferenceEquals(x, y)) return true;

                //Check whether the products' properties are equal. 
                return x != null && y != null && x.GroupLevel.Equals(y.GroupLevel) && x.Permit.Equals(y.Permit);
            }

            public int GetHashCode(PermitsList obj)
            {
                //Get hash code for the Permit field if it is not null. 
                int hashPermit = obj.Permit == null ? 0 : obj.Permit.GetHashCode();

                //Get hash code for the GroupLevel field. 
                int hashGroupLevel = obj.GroupLevel.GetHashCode();

                //Calculate the hash code for the product. 
                return hashPermit ^ hashGroupLevel;
            }
        }
*/
        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
