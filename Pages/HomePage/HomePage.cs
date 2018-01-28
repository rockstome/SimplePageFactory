﻿using OpenQA.Selenium;

namespace SimplePageFactory.Pages
{
    public partial class HomePage : BasePage
    {
        private string Url => "http://newtours.demoaut.com/";

        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver.Navigate().GoToUrl(Url);
        }

        public FlightPage Login(string userName, string password)
        {
            userNameField.SendKeys(userName);
            passwordField.SendKeys(password);
            signInButton.Click();
            return new FlightPage(driver);
        }

        // TODO : delete me
        public void FakeAction()
        {
            userNameField.SendKeys("fake data");
            passwordField.Clear();
            signInButton.Click();
        }
    }
}
