using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SimplePageFactory
{
    class GrowlEventFiringDriver
    {
        private const int delay = 1000;

        public EventFiringWebDriver GetInstance(IWebDriver driver)
        {
            var Driver = new EventFiringWebDriver(driver);

            Driver.FindingElement += FindingElement;
            Driver.ElementValueChanging += ElementValueChanging;

            return Driver;
        }

        private void ElementValueChanging(object sender, WebElementEventArgs e)
        {
            var message = $"Old value is: {e.Element.GetAttribute("value")}";
            ((IJavaScriptExecutor)e.Driver)
                .ExecuteScript("$.growl({ title: 'Element value changing', message: arguments[0] });", message);
            Thread.Sleep(delay);
        }

        private void FindingElement(object sender, FindElementEventArgs e)
        {
            var message = $"Before to find element {e.FindMethod}";
            ((IJavaScriptExecutor)e.Driver)
                .ExecuteScript("$.growl({ title: 'Finding element', message: arguments[0] });", message);
            Thread.Sleep(delay);
        }
    }

    public static class DriverExtensions
    {
        public static void InjectGrowl(this EventFiringWebDriver ed)
        {
            ((IJavaScriptExecutor)ed).ExecuteScript("if (!window.jQuery) {" +
                "var jquery = document.createElement('script'); jquery.type = 'text/javascript';" +
                "jquery.src = 'https://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js';" +
                "document.getElementsByTagName('head')[0].appendChild(jquery);}");

            WaitHelper.WaitForJsToLoad(ed);

            ((IJavaScriptExecutor)ed).ExecuteScript("$.getScript('http://the-internet.herokuapp.com/js/vendor/jquery.growl.js')");

            WaitHelper.WaitForJsToLoad(ed);

            ((IJavaScriptExecutor)ed).ExecuteScript(
                "$('head').append('<link rel=\"stylesheet\" " +
                "href=\"http://the-internet.herokuapp.com/css/jquery.growl.css\" type=\"text/css\" />');");

            WaitHelper.WaitForJsToLoad(ed);
        }
    }

    class WaitHelper
    {
        public static bool WaitForJsToLoad(IWebDriver webDriver)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(30));

            Func<IWebDriver, bool> JsLoad = (d) =>
            {
                return (string)((IJavaScriptExecutor)d).ExecuteScript("return document.readyState;") == "complete";
            };

            Func<IWebDriver, bool> jQueryLoad = (d) =>
            {
                try
                {
                    return (Int64)((IJavaScriptExecutor)d).ExecuteScript("return jQuery.active;") == 0;
                }
                catch (Exception e)
                {
                    return false;
                }
            };

            return wait.Until(JsLoad) && wait.Until(jQueryLoad);
        }
    }

    class GrowlTest
    {
        EventFiringWebDriver eventDriver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            eventDriver = new GrowlEventFiringDriver().GetInstance(new ChromeDriver());
        }

        [Test]
        public void FillTheForm()
        {
            eventDriver.Navigate().GoToUrl(@"C:\Users\tomas\Documents\Visual Studio 2015\Projects\Tomasz\SimplePageFactory\Sites\formPage.html");

            eventDriver.InjectGrowl();

            var firstname = eventDriver.FindElement(By.Name("firstname"));
            firstname.Clear();
            firstname.SendKeys("Tomasz");

            var lastname = eventDriver.FindElement(By.Name("lastname"));
            lastname.Clear();
            lastname.SendKeys("Solarz");
        }
    }
}
