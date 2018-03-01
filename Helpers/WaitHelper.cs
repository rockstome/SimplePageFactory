using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SimplePageFactory.Helpers
{
    public class WaitHelper
    {
        private WaitHelper()
        {

        }

        public static bool WaitForTextPresentedInField(IWebDriver d, IWebElement e, string text, int time = 30)
        {
            return new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(TextPresentedInField(e, text));
        }

        public static string WaitForSomeTextPresentedInField(IWebDriver d, IWebElement e, int time = 30)
        {
            return new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(SomeTextPresentedInField(e));
        }
         
        private static Func<IWebDriver, bool> TextPresentedInField(IWebElement e, string text)
        {
            return (d) =>
            {
                try
                {
                    var value = e.GetAttribute("value");
                    return String.Equals(value, text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        private static Func<IWebDriver, string> SomeTextPresentedInField(IWebElement e)
        {
            return (d) =>
            {
                try
                {
                    var value = e.GetAttribute("value");
                    if (value.Length != 0) return value;
                    else return null;
                }
                catch(StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        [Test]
        public void Test()
        {
            var d = new ChromeDriver();
            d.Navigate().GoToUrl("data:text/html, <h1>Write 'elo' to first input and click button<h1/>" +
                " <input id='input1' value=''/><br/><button onclick='myFunction()'>Copy to second field</button> " +
                "<br/> <input id='input2' value='' disabled/> <script> function myFunction()" +
                " { document.getElementById('input2').value = document.getElementById('input1').value; } </script>");
            IWebElement e = d.FindElement(By.Id("input2"));
            WaitHelper.WaitForTextPresentedInField(d, e, "elo");
        }
    }
}
