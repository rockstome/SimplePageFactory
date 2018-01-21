using OpenQA.Selenium;

namespace SimplePageFactory.Pages.Sections
{
    public partial class MainMenuSection : BaseSection
    {
        public MainMenuSection(IWebDriver driver) : base(driver) { }

        public HomePage GoToHomePage()
        {
            homeLink.Click();
            return new HomePage(driver);
        }

        public FlightPage GoToFlightPage()
        {
            flightsLink.Click();
            return new FlightPage(driver);
        }

        // TODO: implement 5 other navigation methods
    }
}
