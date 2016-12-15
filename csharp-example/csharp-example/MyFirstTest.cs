using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class MyFirstTest : BaseTestfixture
    {

        [Test]
        public void Firsttest()
        {
            Driver.Url = "http://www.google.ru/";
            Driver.FindElement(By.Name("q")).SendKeys("webdriver");
            Driver.FindElement(By.Name("btnG")).Click();
            Wait.Until(ExpectedConditions.TitleContains("webdriver"));
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
    }
}
