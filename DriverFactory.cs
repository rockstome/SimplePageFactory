using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SimplePageFactory
{
    public enum Browsers
    {
        Chrome, Firefox
    }

    public class DriverFactory
    {
        public static IWebDriver Create(Browsers browser)
        {
            IWebDriver driver = null;
            switch (browser)
            {
                case Browsers.Chrome:
                    driver = new ChromeDriver();
                    break;
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    break;
            }
            return driver;
        }
    }
}
