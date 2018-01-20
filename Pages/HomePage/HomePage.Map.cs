using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages
{
    partial class HomePage
    {
        [FindsBy(How = How.Name, Using = "userName")]
        private IWebElement UserNameField;

        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement PasswordField;

        [FindsBy(How = How.Name, Using = "login")]
        private IWebElement SignInButton;
    }
}
