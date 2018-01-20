using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages.Sections
{
    // TODO: should inherit from BaseSection
    partial class MainMenuSection
    {
        private IWebDriver driver;

        public MainMenuSection(IWebDriver driver) {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public HomePage GoToHomePage()
        {
            HomeLink.Click();
            return new HomePage(driver);
        }

        public FlightPage GoToFlightPage()
        {
            FlightsLink.Click();
            return new FlightPage(driver);
        }

        // TODO: implement 5 navigation methods
    }
}
