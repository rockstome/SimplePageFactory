using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages.Sections
{
    partial class MainMenuSection
    {
        [FindsBy(How = How.XPath, Using = "//a[text()='Home']")]
        private IWebElement HomeLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Flights']")]
        private IWebElement FlightsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Hotels']")]
        private IWebElement HotelsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Car Rentals']")]
        private IWebElement CarRentalsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Cruises']")]
        private IWebElement CruisesLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Destinations']")]
        private IWebElement DestinationsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Vacations']")]
        private IWebElement VacationsLink;
    }
}
