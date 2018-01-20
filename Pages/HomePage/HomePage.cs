using OpenQA.Selenium;

namespace SimplePageFactory.Pages
{
    partial class HomePage : BasePage
    {
        private string Url => "http://newtours.demoaut.com/";

        public HomePage(IWebDriver driver) : base(driver)
        {
            this.driver.Navigate().GoToUrl(Url);
        }

        public FlightPage Login(string userName, string password)
        {
            UserNameField.SendKeys(userName);
            PasswordField.SendKeys(password);
            SignInButton.Click();
            return new FlightPage(driver);
        }
    }
}
