using OpenQA.Selenium;
using System;

namespace SimplePageFactory.Pages
{
    public partial class HomePage : BasePage
    {
        protected override string Url => "http://newtours.demoaut.com/";

        public HomePage(IWebDriver driver) : base(driver)
        {
            // TODO navigating in constructor is a bad idea
            this.driver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// Performs logging into the site.
        /// </summary>
        /// <typeparam name="TPage">Available pages: <see cref="FlightPage"/>, <see cref="SignOnPage"/>.</typeparam>
        /// <param name="userName">User name to be typed into field.</param>
        /// <param name="password">Password to be typed into field.</param>
        /// <returns>Page to be redirected.</returns>
        public TPage Login<TPage>(string userName, string password) where TPage : BasePage
        {
            userNameField.SendKeys(userName);
            passwordField.SendKeys(password);
            signInButton.Click();
            return (TPage)Activator.CreateInstance(typeof(TPage), driver);
        }
    }
}
