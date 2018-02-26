using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace SimplePageFactory
{
    /// <summary>
    /// Implementation of factory, used to create objects. 
    /// </summary>
    public class EventFiringDriverFactory
    {
        private Logger logger;

        /// <summary>
        /// Method for creating an instance of <see cref="EventFiringWebDriver"/> 
        /// with the ability to logging to NLog.
        /// </summary>
        /// <param name="driver">The driver to register events for.</param>
        /// <param name="logger">The logger to register events to.</param>
        /// <returns>Driver with possibility to register events to logger.</returns>
        public EventFiringWebDriver CreateInstance(IWebDriver driver, Logger logger)
        {
            this.logger = logger;
            var Driver = new EventFiringWebDriver(driver);

            // set condition to 'false' to disable firing on event
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

                Driver.ExceptionThrown += ExceptionThrown;

                Driver.FindingElement += FindingElement;
                Driver.FindElementCompleted += FindElementCompleted;

                Driver.ElementClicking += ElementClicking;
                Driver.ElementClicked += ElementClicked;

                Driver.ElementValueChanging += ElementValueChanging;
                Driver.ElementValueChanged += ElementValueChanged;
            }
            return Driver;
        }

        private void Navigating(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"Before navigating to: {e.Url}, I was at: {e.Driver.Url}");
        }

        private void Navigated(object sender, WebDriverNavigationEventArgs e)
        {
            logger.Debug($"After navigating to: {e.Url}, I'm at: {e.Driver.Url}");
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

        private void ScriptExecuting(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, before executing script: {e.Script}");
        }

        private void ScriptExecuted(object sender, WebDriverScriptEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, after executed script: {e.Script}");
        }

        private void ExceptionThrown(object sender, WebDriverExceptionEventArgs e)
        {
            //exceptions are logged in teardown
        }

        private void FindingElement(object sender, FindElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, before to find element {e.FindMethod}");
        }

        private void FindElementCompleted(object sender, FindElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, after finding element {e.FindMethod}");
        }

        private void ElementClicking(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, before to click on element with tag: {e.Element.TagName}, " +
                $"innerText: {e.Element.Text}, location: {e.Element.Location.ToString()}, displayed: {e.Element.Displayed}, " +
                $"enabled: {e.Element.Enabled},selected: {e.Element.Selected}, size: {e.Element.Size.ToString()}");
        }

        private void ElementClicked(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, after clicking on element");
        }

        private void ElementValueChanging(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, before to change value of element, old value was: '{e.Element.GetAttribute("value")}'");
        }

        private void ElementValueChanged(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On: {e.Driver.Url}, after changing value of element, new value is: '{e.Element.GetAttribute("value")}'");
        }
    }
}
