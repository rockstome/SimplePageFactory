using NUnit.Framework;
using SimplePageFactory.Pages;

namespace SimplePageFactory.Tests
{
    class LoginTest : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            htmlLogger.Info("1. Open homepage");
            var homePage = new HomePage(Driver);


            htmlLogger.Info("2. Input valid credentials");
            var flightPage = homePage.Login<FlightPage>(userName: "a", password: "a");

            Assert.That(false);
        }
    }
}
