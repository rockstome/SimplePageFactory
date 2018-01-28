using NUnit.Framework;
using SimplePageFactory.Pages;

namespace SimplePageFactory.Tests
{
    class LoginTest2 : BaseTest
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            var homePage = new HomePage(Driver);
            homePage.AssertIsAt();

            var flightPage = homePage.Login("a", "a");
            flightPage.AssertIsAt();
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
