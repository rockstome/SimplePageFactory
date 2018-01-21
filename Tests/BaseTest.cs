using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SimplePageFactory.Tests
{
    class BaseTest
    {
        protected IWebDriver Driver { get; private set; }

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            Driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            Driver.Quit();
        }
    }
}
