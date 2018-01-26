using OpenQA.Selenium;

namespace SimplePageFactory.Helpers
{
    public static class WebElementHelper
    {
        /// <summary>
        /// When checkbox is off clicks on it, otherwise no action is made.
        /// </summary>
        public static void CheckboxOn(this IWebElement element)
        {
            if (!element.Selected)
                element.Click();
        }

        /// <summary>
        /// When checkbox is on clicks on it, otherwise no action is made.
        /// </summary>
        public static void CheckboxOff(this IWebElement element)
        {
            if (element.Selected)
                element.Click();
        }

        /// <summary>
        /// Click on IWebElement by execute script "arguments[0].click();".
        /// </summary>
        public static void JsClick(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
