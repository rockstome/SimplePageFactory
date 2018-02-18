using OpenQA.Selenium;

namespace SimplePageFactory.Pages
{
    public partial class SignOnPage : BasePage, IHomePageLogin
    {
        protected override string Url => "http://newtours.demoaut.com/mercurysignon.php";

        public SignOnPage(IWebDriver driver) : base(driver) { }
    }
}
