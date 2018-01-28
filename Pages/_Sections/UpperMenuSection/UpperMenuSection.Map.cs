using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages.Sections
{
    partial class UpperMenuSection
    {
        [FindsBy(How = How.XPath, Using = "//a[text()='SIGN-OFF']")]
        private IWebElement signOffLink;

        [FindsBy(How = How.XPath, Using = "//a[text()='SIGN-ON']")]
        private IWebElement signOnLink;
    }
}
