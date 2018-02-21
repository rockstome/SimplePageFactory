using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SimplePageFactory.Helpers;

namespace SimplePageFactory.Tests._FrameworkTests
{
    [TestFixture]
    public class WebElementHelperTests
    {
        IWebDriver d;
        IWebElement e;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            d = new ChromeDriver();
        }

        [SetUp]
        public void SetUp()
        {
            d.Navigate().GoToUrl("about:blank");
        }

        [Test, Category("WebElementHelperTests")]
        public void JsGetInputValue()
        {
            d.Navigate().GoToUrl(@"data:text/html,<div><input id='input' ></input>");
            e = d.FindElement(By.Id("input"));

            // read input value by js and compare to sended text
            string text = "some text";
            e.SendKeys(text);
            Assert.That(e.JsGetInputValue(d), Is.EqualTo(text));

            // clear, read value by js and compare with empty string
            e.Clear();
            Assert.That(e.JsGetInputValue(d), Is.EqualTo(string.Empty));
        }

        [Test, Category("WebElementHelperTests")]
        public void JsClear()
        {
            d.Navigate().GoToUrl(@"data:text/html,<div><input id='input' ></input>");
            e = d.FindElement(By.Id("input"));

            // check if text is in input
            string text = "some text";
            e.SendKeys(text);
            Assert.That(e.GetAttribute("value"), Is.EqualTo(text));

            // check if input is empty
            e.JsClear(d);
            Assert.That(e.GetAttribute("value"), Is.EqualTo(string.Empty));
        }

        [Test, Category("WebElementHelperTests")]
        public void JsSendKeys()
        {
            d.Navigate().GoToUrl(@"data:text/html,<div><input id='input' ></input>");
            e = d.FindElement(By.Id("input"));

            // check if text is in input
            string text = "some text";
            e.JsSendKeys(d, text);
            Assert.That(e.GetAttribute("value"), Is.EqualTo(text));
        }

        [Test, Category("WebElementHelperTests")]
        public void JsHighlightParent()
        {
            d.Navigate().GoToUrl(@"data:text/html,<div><h1>Hello</h1><h1 id='h1'>World</h1>");
            e = d.FindElement(By.Id("h1"));

            IWebElement parentNode = d.FindElement(By.XPath("//*[@id='h1']/.."));

            // check if parent has no style
            Assert.That(parentNode.GetAttribute("style"), Is.EqualTo(string.Empty));

            // check if parent has style
            e.JsHighlightParent(d);
            Assert.That(parentNode.GetAttribute("style"), Is.EqualTo("border: 3px solid red;"));

            // check if we cant still use this element
            Assert.That(e.Text, Is.EqualTo("World"));

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            d.Quit();
        }
    }
}
