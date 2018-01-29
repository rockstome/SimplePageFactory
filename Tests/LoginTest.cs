using NUnit.Framework;
using OpenQA.Selenium;
using SimplePageFactory.Pages;
using System.Threading;

namespace SimplePageFactory.Tests
{
    class LoginTest : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            Driver.Navigate().GoToUrl("https://www.w3schools.com/jsref/met_element_setattribute.asp");

            int i = 2;
            while (i-- > 0)
            {
                var e = Driver.FindElement(By.XPath("//*[@id='main']/h1"));
                e.Click();
            }
            Thread.Sleep(3000);
        }

        [Test]
        public void LoginWithValidCredentials2()
        {
            var homePage = new HomePage(Driver);
            homePage.AssertIsAt();

            var flightPage = homePage.Login("a", "a");
            flightPage.AssertIsAt();
        }
    }
}
