using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SimplePageFactory.Tests
{
    class BaseTest
    {
        protected IWebDriver driver { get; set; }

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            driver = new ChromeDriver();
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            driver.Quit();
        }
    }
}
