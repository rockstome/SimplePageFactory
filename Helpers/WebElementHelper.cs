using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Threading;

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
        public static void JsClick(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].click();", element);
        }

        /// <summary>
        /// Simulates typing text into the element by appending text to element's value attribute.
        /// </summary>
        /// <param name="text">The text to type into the element.</param>
        public static void JsSendKeys(this IWebElement element, string text)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
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
        public static void JsClear(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].value = '';", element);
        }

        /// <summary>
        /// Removes keyboard focus from this element
        /// </summary>
        public static void JsBlur(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].blur();", element);
        }

        /// <summary>
        /// Sets focus on this element, if it can be focused.
        /// </summary>
        public static void JsFocus(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("return arguments[0].focus();", element);
        }

        // TODO: check if works also with textareas
        /// <summary>
        /// Gets value from this input.
        /// </summary>
        /// <returns>Value from input.</returns>
        public static string JsGetInputValue(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return (string)executor.ExecuteScript("return arguments[0].value", element);
        }

        /// <summary>
        /// Sets red solid border on parent node.
        /// Wait a moment and set style to previous.
        /// </summary>
        public static void JsHighlightParent(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            object oldStyle = executor.ExecuteScript("return arguments[0].parentNode.style", element);
            // TODO: new style should be appended to old style. not replaced
            executor.ExecuteScript("return arguments[0].parentNode.style.border='3px solid red'", element);
            Thread.Sleep(1000);
            executor.ExecuteScript("return arguments[0].parentNode.style.border=arguments[1]", element, oldStyle);
        }

        // https://www.w3schools.com/jsref/dom_obj_all.asp
    }
}