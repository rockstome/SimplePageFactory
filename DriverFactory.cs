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
                    ChromeOptions options = new ChromeOptions();
                    options.SetLoggingPreference(LogType.Browser, LogLevel.All);
                    driver = new ChromeDriver(options);
                    break;
                case Browsers.Firefox:
                    driver = new FirefoxDriver();
                    break;
                default:
                    break;
            }
            driver.Manage().Window.Size = new System.Drawing.Size(1366, 768);
            return driver;
        }
    }
}
