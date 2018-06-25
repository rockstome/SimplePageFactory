using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace SimplePageFactory.Helpers
{
    public static class WebElementHelper
    {
        /// <summary>
        /// Paste text into web element.
        /// </summary>
        public static void PasteText(this IWebElement element, IWebDriver driver, string text)
        {
            element.Clear();
            Thread thread = new Thread(() => Clipboard.SetText(text));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
            new Actions(driver)
                .MoveToElement(element)
                .Click()
                .SendKeys(OpenQA.Selenium.Keys.Control + "v")
                .Perform();
        }
        
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
            // TODO: new style should be appended to old style, not replaced
            executor.ExecuteScript("return arguments[0].parentNode.style.border='3px solid red'", element);
            Thread.Sleep(1000);
            executor.ExecuteScript("return arguments[0].parentNode.style.border=arguments[1]", element, oldStyle);
        }

        /// <summary>
        /// Get text of element.
        /// </summary>
        /// <returns>Text of element.</returns>
        public static string JsGetText(this IWebElement element)
        {
            IWebDriver driver = ((RemoteWebElement)element).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            return (string)executor.ExecuteScript("return arguments[0].text", element);
        }

        public static void JsDragAndDrop(this IWebElement source, IWebElement target)
        {
            string script = @"
                function createEvent(typeOfEvent) {
                    var event = document.createEvent(""CustomEvent"");
                    event.initCustomEvent(typeOfEvent, true, true, null);
                    event.dataTransfer = {
                        data: { },
                        setData: function(key, value) {
                            this.data[key] = value;
                        },
                        getData: function(key) {
                            return this.data[key];
                        }
                    };
                    return event;
                }
                function dispatchEvent(element, event, transferData) {
                    if (transferData !== undefined)
                    {
                        event.dataTransfer = transferData;
                    }
                    if (element.dispatchEvent) {
                        element.dispatchEvent(event);
                    } else if (element.fireEvent) {
                        element.fireEvent(""on"" + event.type, event);
                    }
                }
                function simulateHTML5DragAndDrop(element, target)
                {
                    var dragStartEvent = createEvent('dragstart');
                    dispatchEvent(element, dragStartEvent);
                    var dropEvent = createEvent('drop');
                    dispatchEvent(target, dropEvent, dragStartEvent.dataTransfer);
                    var dragEndEvent = createEvent('dragend');
                    dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);
                }
                return simulateHTML5DragAndDrop(arguments[0], arguments[1])";

            IWebDriver driver = ((RemoteWebElement)target).WrappedDriver;
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript(script, source, target);
        }

        // https://www.w3schools.com/jsref/dom_obj_all.asp
    }
}
