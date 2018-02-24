using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;

namespace SimplePageFactory
{
    public class EventFiringDriverFactory
    {
        private Logger logger;

        public EventFiringWebDriver CreateInstance(IWebDriver driver, Logger logger)
        {
            this.logger = logger;
            var Driver = new EventFiringWebDriver(driver);
            if (true)
            {
                Driver.Navigating += Navigating;
                Driver.Navigated += Navigated;

                Driver.NavigatingBack += NavigatingBack;
                Driver.NavigatedBack += NavigatedBack;

                Driver.NavigatingForward += NavigatingForward;
                Driver.NavigatedForward += NavigatedForward;

                Driver.ScriptExecuting += ScriptExecuting;
                Driver.ScriptExecuted += ScriptExecuted;
            }
            return Driver;
        }

        private void ScriptExecuting(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"Before executing script: {e.Script}");
        }

        private void ScriptExecuted(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"After executed script: {e.Script}");
        }

        private void Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating to: {e.Url}, my url was: {e.Driver.Url}");
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating to: {e.Url}, my url is: {e.Driver.Url}");
        }

        private void NavigatingBack(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating back, I was at: {e.Driver.Url}");
        }

        private void NavigatedBack(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating back, I'm at: {e.Driver.Url}");
        }

        private void NavigatingForward(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating forward, I was at: {e.Driver.Url}");
        }

        private void NavigatedForward(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating forward, I'm at: {e.Driver.Url}");
        }







        private void ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            // TODO check if this works on assertion and regular exceptions
            logger.Debug($"Exception was thrown. {e.ThrownException.ToString()}");
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


    }
}
