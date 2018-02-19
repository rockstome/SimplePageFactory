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
        /// Click this element by calling JavaScript click() method".
        /// </summary>
        public static void JsClick(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].click();", element);
        }

        /// <summary>
        /// Simulates typing text into the element by appending text to element's value attribute.
        /// </summary>
        /// <param name="driver">The driver to execute JavaScript script.</param>
        /// <param name="text">The text to type into the element.</param>
        public static void JsSendKeys(this IWebElement element, IWebDriver driver, string text)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript(
                "var element = arguments[0];" +
                "var text = arguments[1];" +
                "return element.value += text;"
                , element, text);
        }

        /// <summary>
        /// Clears the content of this element
        /// </summary>
        /// <param name="driver">The driver to execute JavaScript script.</param>
        public static void JsClear(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].value = '';", element);
        }

        /// <summary>
        /// Removes keyboard focus from this element
        /// </summary>
        /// <param name="driver">The driver to execute JavaScript script.</param>
        public static void JsBlur(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].blur();", element);
        }

        /// <summary>
        /// Sets focus on this element, if it can be focused.
        /// </summary>
        /// <param name="driver">The driver to execute JavaScript script.</param>
        public static void JsFocus(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].focus();", element);
        }

        // TODO: check if works also with textareas
        /// <summary>
        /// Gets value from this input.
        /// </summary>
        /// <param name="driver">The driver to execute JavaScript script.</param>
        /// <returns>Value from input.</returns>
        public static string JsGetInputValue(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return (string)executor.ExecuteScript("return arguments[0].value", element);
        }

        /// <summary>
        /// Sets red solid border on parent node.
        /// </summary>
        /// <param name="driver"></param>
        public static void JsHighlightParent(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].parentNode.style.border='3px solid red'", element);
        }

        // https://www.w3schools.com/jsref/dom_obj_all.asp
        // get the value of an element
        // highlight an element
    }
}
