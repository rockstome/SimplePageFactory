using NUnit.Framework;
using OpenQA.Selenium;
using SimplePageFactory.Pages;
using System.Threading;

namespace SimplePageFactory.Tests
{
    class LoginTest : BaseTest
    {
        [Test]
        [TestCase("a", "a")]
        public void LoginWithValidCredentials(string userName, string password)
        {
            htmlLogger.Info("1. Open homepage");
            var homePage = new HomePage(Driver);

            htmlLogger.Info("2. Input valid credentials");
            var flightPage = homePage.Login(userName, password);

            htmlLogger.Info("3. Verify user is logged in");
            flightPage.AssertIsAt();

            Assert.That(false);
        }
    }
}
