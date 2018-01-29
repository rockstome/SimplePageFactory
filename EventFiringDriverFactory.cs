using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace SimplePageFactory
{
    public class EventFiringDriverFactory
    {
        Logger logger;

        public EventFiringWebDriver Create(IWebDriver driver, Logger logger=null)
        {
            var Driver = new EventFiringWebDriver(driver);
            if (logger != null)
            {
                Driver.ElementClicking += Driver_ElementClicking;
                Driver.ElementValueChanged += Driver_ElementValueChanged;
                Driver.FindingElement += Driver_FindingElement;
                Driver.FindElementCompleted += Driver_FindElementCompleted;
                Driver.Navigating += Driver_Navigating;
                Driver.Navigated += Driver_Navigated;
                Driver.ExceptionThrown += Driver_ExceptionThrown;
                Driver.ScriptExecuting += Driver_ScriptExecuting;
            }
            return Driver;
        }


        private void Driver_ScriptExecuting(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"Executing script: {e.Script}");
        }

        private void Driver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            // https://seleniumjava.com/2016/09/05/how-to-execute-javascript-code-in-selenium-webdriver/
            // TODO: screen, js border on outer element, log
            // TODO check if this works on assertion and regular exceptions
        }

        private void Driver_Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating to: {e.Url}, my url is: {e.Driver.Url}");
        }

        private void Driver_Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating to: {e.Url}, my url was: {e.Driver.Url}");
        }

        private void Driver_FindElementCompleted(object sender, FindElementEventArgs e)
        {
            logger.Debug($"Complete to find element {e.FindMethod}");
        }

        private void Driver_FindingElement(object sender, FindElementEventArgs e)
        {
            logger.Debug($"Prepare to find element {e.FindMethod}");
        }

        private void Driver_ElementValueChanged(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On website: {e.Driver.Url} changed value of element with tag name: '{e.Element.TagName}', actual value: '{e.Element.GetAttribute("value")}'");
        }

        private void Driver_ElementClicking(object sender, WebElementEventArgs e)
        {
            //logger.Debug("Clicking on element with tag name: " + e.Element.TagName);

            IJavaScriptExecutor js = (IJavaScriptExecutor)(e.Driver);
            js.ExecuteScript(
                @"arguments[0].setAttribute(""style"", arguments[1]);",
                e.Element,
                //"border: 3px solid red;"
                "color: yellow;"
                );
        }
    }
}
