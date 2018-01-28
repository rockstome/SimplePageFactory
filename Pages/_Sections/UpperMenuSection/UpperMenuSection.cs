using OpenQA.Selenium;

namespace SimplePageFactory.Pages.Sections
{
    public partial class UpperMenuSection : BaseSection
    {
        public UpperMenuSection(IWebDriver driver) : base(driver) { }

        public SignOnPage SignOff()
        {
            signOffLink.Click();
            return new SignOnPage(driver);
        }

        // methods 
    }
}
