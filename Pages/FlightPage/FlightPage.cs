using OpenQA.Selenium;

namespace SimplePageFactory.Pages
{
    public partial class FlightPage : BasePage
    {
        private string Url => "http://newtours.demoaut.com/mercuryreservation.php";

        public FlightPage(IWebDriver driver) : base(driver) { }
    }
}
