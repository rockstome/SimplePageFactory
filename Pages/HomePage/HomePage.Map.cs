using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages
{
    partial class HomePage
    {
        [FindsBy(How = How.Name, Using = "userName")]
        private IWebElement userNameField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement passwordField;

        [FindsBy(How = How.Name, Using = "login")]
        private IWebElement signInButton;
    }
}
