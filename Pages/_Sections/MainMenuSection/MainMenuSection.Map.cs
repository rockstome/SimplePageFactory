using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages.Sections
{
    partial class MainMenuSection
    {
        [FindsBy(How = How.XPath, Using = "//a[text()='Home']")]
        private IWebElement homeLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Flights']")]
        private IWebElement flightsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Hotels']")]
        private IWebElement hotelsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Car Rentals']")]
        private IWebElement carRentalsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Cruises']")]
        private IWebElement cruisesLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Destinations']")]
        private IWebElement destinationsLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='Vacations']")]
        private IWebElement vacationsLink;
    }
}
