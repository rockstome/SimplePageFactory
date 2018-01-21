using NUnit.Framework;
using SimplePageFactory.Pages;

namespace SimplePageFactory.Tests
{
    class LoginTest : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            var homePage = new HomePage(Driver);
            homePage.AssertIsAt();

            var flightPage = homePage.Login("a", "a");
            flightPage.AssertIsAt();

            flightPage.MainMenuSection.GoToHomePage();
            homePage.AssertIsAt();
        }
    }
}
