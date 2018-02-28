using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SimplePageFactory.Helpers
{
    public class WaitHelper
    {
        public static void WaitForTextPresentedInField(IWebDriver d, IWebElement e, string text, int time = 10)
        {
            new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(TextPresentedInField(e, text));
        }

        public static string  WaitForTextPresentedInField2(IWebDriver d, IWebElement e, string text, int time = 30)
        {
            return new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(TextPresentedInField(e, text));
        }

        public static Func<IWebDriver, string> TextPresentedInField(IWebElement e, string text)
        {
            return (d) => {
                var value = e.GetAttribute("value");
                if (value.Length != 0)
                    return value;
                return null;
            };
        }

        [Test]
        public void Test()
        {
            var d = new ChromeDriver();
            d.Navigate().GoToUrl("data:text/html,<input id='input' value=''/>");
            IWebElement e = d.FindElement(By.Id("input"));
            WaitHelper.WaitForTextPresentedInField2(d, e, "elo");
            //WaitForTextPresentedInField(d, By.Id("input"), "elo");
        }
    }
}
