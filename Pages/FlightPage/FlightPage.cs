using OpenQA.Selenium;

namespace SimplePageFactory.Pages
{
    public partial class FlightPage : BasePage
    {
        protected override string Url => "http://newtours.demoaut.com/mercuryreservation.php";

        public FlightPage(IWebDriver driver) : base(driver) { }

        public FlightPage DoNothing()
        {
            return new FlightPage(driver);
        }
    }
}
