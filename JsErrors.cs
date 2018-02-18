using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleniumFeatures2
{
    public class Program
    {
        static List<string> err = new List<string>();

        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            var _driver = new ChromeDriver(options);

            var driver = new EventFiringWebDriver(_driver);
            driver.ElementClicked += Driver_ElementClicked;
            driver.Navigated += Driver_Navigated;

            driver.Navigate().GoToUrl(@"C:\Users\tomas\Documents\Visual Studio 2015\Projects\Tomasz\SimplePageFactory\Sites\jserrors.html");
            driver.FindElement(By.Id("range")).Click();
            driver.FindElement(By.Id("syntax")).Click();

            driver.Navigate().Refresh();
        }

        private static void Driver_Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            var errors = JsErrors(e.Driver);

            if (!string.IsNullOrEmpty(errors))
                err.Add(errors);
        }

        private static void Driver_ElementClicked(object sender, WebElementEventArgs e)
        {
            var errors = JsErrors(e.Driver);

            if (!string.IsNullOrEmpty(errors))
                err.Add(errors);
        }

        public static string JsErrors(IWebDriver driver)
        {
            var errorStrings = new List<string>
            {
                "SyntaxError",
                "EvalError",
                "ReferenceError",
                "RangeError",
                "TypeError",
                "URIError",
                "InternalError"
            };

            var jsErrors = driver.Manage().Logs.GetLog(LogType.Browser).Where(x => errorStrings.Any(e => x.Message.Contains(e)));
            string record = "";

            if (jsErrors.Any())
            {
                record = "On page: " + driver.Url + Environment.NewLine +
                    "JavaScript error(s):" + Environment.NewLine +
                    jsErrors.Aggregate("", (s, entry) => s + entry.Message + Environment.NewLine);
            }

            return record;
        }

		public static void SaveToFile(string contents, string path)
        {
            if (!String.IsNullOrEmpty(contents))
                System.IO.File.AppendAllText(path, contents);
        }
    }    
}
