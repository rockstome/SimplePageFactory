using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;

namespace SimplePageFactory
{
    public class EventFiringDriverFactory
    {
        //public EventFiringWebDriver Create(IWebDriver driver)
        //{
        //    var Driver = new EventFiringWebDriver(driver);

        //    Driver.ExceptionThrown += Driver_ExceptionThrown;

        //    return Driver;
        //}

        //private void Driver_ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        //{
        //    var timeAndDate = DateTime.Now.ToString("yyyyMMdd_hhmmss");
        //    var driver = e.Driver;
        //    Exception ex = e.ThrownException;
        //    ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(timeAndDate + ".PNG");
        //}


        private Logger logger;

        public EventFiringWebDriver CreateInstance(IWebDriver driver, Logger logger)
        {
            this.logger = logger;
            var Driver = new EventFiringWebDriver(driver);
            if (true)
            {
                Driver.Navigating += Navigating;
                Driver.Navigated += Navigated;

                Driver.NavigatingBack += Driver_NavigatingBack;
                Driver.NavigatedBack += Driver_NavigatedBack;

                Driver.NavigatingForward += Driver_NavigatingForward;
                Driver.NavigatedForward += Driver_NavigatedForward;

                Driver.ElementClicking += ElementClicking;
                Driver.ElementValueChanged += ElementValueChanged;
                Driver.FindingElement += FindingElement;
                Driver.FindElementCompleted += FindElementCompleted;
                Driver.ExceptionThrown += ExceptionThrown;
                Driver.ScriptExecuting += ScriptExecuting;
            }
            return Driver;
        }

        private void Driver_NavigatedForward(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating forward to: {e.Url}, my url is: {e.Driver.Url}");
        }

        private void Driver_NavigatingForward(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating forward to: {e.Url}, my url was: {e.Driver.Url}");
        }

        private void Driver_NavigatedBack(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating back to: {e.Url}, my url is: {e.Driver.Url}");
        }

        private void Driver_NavigatingBack(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating back to: {e.Url}, my url was: {e.Driver.Url}");
        }

        private void ScriptExecuting(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"Executing script: {e.Script}");
        }

        private void ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            // https://seleniumjava.com/2016/09/05/how-to-execute-javascript-code-in-selenium-webdriver/
            // TODO: screen, js border on outer element, log
            // TODO check if this works on assertion and regular exceptions
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating to: {e.Url}, my url is: {e.Driver.Url}");
        }

        private void Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating to: {e.Url}, my url was: {e.Driver.Url}");
        }

        private void FindElementCompleted(object sender, FindElementEventArgs e)
        {
            logger.Debug($"Complete to find element {e.FindMethod}");
        }

        private void FindingElement(object sender, FindElementEventArgs e)
        {
            logger.Debug($"Prepare to find element {e.FindMethod}");
        }

        private void ElementValueChanged(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On website: {e.Driver.Url} changed value of element with tag name: '{e.Element.TagName}', actual value: '{e.Element.GetAttribute("value")}'");
        }

        private void ElementClicking(object sender, WebElementEventArgs e)
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
