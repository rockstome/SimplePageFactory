using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SimplePageFactory.Pages.Sections
{
    public abstract class BaseSection
    {
        protected IWebDriver driver;

        public BaseSection(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
    }
}
