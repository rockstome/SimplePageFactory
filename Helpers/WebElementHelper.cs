using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SimplePageFactory.Helpers
{
    public static class WebElementHelper
    {
        /// <summary>
        /// Checks this checkbox if not checked already.
        /// </summary>
        public static void Check(this IWebElement element)
        {
            if (!element.Selected)
                element.Click();
        }

        /// <summary>
        /// Unchecks this checkbox if not unchecked already.
        /// </summary>
        public static void Uncheck(this IWebElement element)
        {
            if (element.Selected)
                element.Click();
        }

        /// <summary>
        /// Selects the option that has a value matching the argument.
        /// </summary>
        /// <param name="value">The value to match against.</param>
        public static void SelectByValue(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByValue(value);
        }

        /// <summary>
        /// Click on IWebElement by calling JavaScript click() method".
        /// </summary>
        public static void JsClick(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", element);
        }

        // TODO: blur, focus
        // https://www.w3schools.com/jsref/dom_obj_all.asp
        // find element
        // type text into textbox
        // click element
        // get the value of an element
        // highlight an element
        //
        // TODO : is page loaded document.readyState == 'complete'
        // executeScript("return document.readyState").equals("complete");
    }
}
