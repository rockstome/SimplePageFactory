using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using System;

namespace SimplePageFactory.Tests._FrameworkTests
{
    [TestFixture]
    class EventFiringDriverFactoryTests
    {
        EventFiringWebDriver ed;
        IWebDriver d;
        Logger l;
        Logger logger;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            d = new ChromeDriver();
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            l = LogManager.GetLogger("Event Firing Driver Logger");
            logger = LogManager.GetLogger("Test Suite Logger");
            ed = new EventFiringDriverFactory().CreateInstance(d, l);
        }
		
		[SetUp]
        public void SetUp()
        {
            logger.Debug("##############################");
            logger.Debug("Start test: " + TestContext.CurrentContext.Test.FullName);
            d.Navigate().GoToUrl("about:blank");
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Navigation()
        {
            ed.Navigate().GoToUrl("data:text/html,<title>my title</title>");
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void NavigationBack()
        {
            d.Navigate().GoToUrl("data:text/html,<title>page 1</title>");
            d.Navigate().GoToUrl("data:text/html,<title>page 2</title>");
            ed.Navigate().Back();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void NavigationForward()
        {
            d.Navigate().GoToUrl("https://www.wikipedia.org/");
            d.Navigate().Back();
            ed.Navigate().Forward();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Exception1()
        {
            Assert.That(false);
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Exception2()
        {
            ed.FindElement(By.Id("fake id"));
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Exception3()
        {
            throw new FormatException("outer", new IndexOutOfRangeException("inner"));
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Script()
        {
            d.Navigate().GoToUrl("data:text/html,<button id='button'>button</button>");
            var e = d.FindElement(By.Id("button"));
            ((IJavaScriptExecutor)ed).ExecuteScript("return arguments[0].click();", e);
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void FindElement()
        {
            d.Navigate().GoToUrl("data:text/html,<button id='button'>button</button>");
            ed.FindElement(By.Id("button"));
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Click()
        {
            d.Navigate().GoToUrl("data:text/html,<button id='button'>button</button>");
            var e = ed.FindElement(By.Id("button"));
            e.Click();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void InputValueChange()
        {
            d.Navigate().GoToUrl("data:text/html,<input id='input' value='some text'/>");
            var e = ed.FindElement(By.Id("input"));
            e.SendKeys(" and some additional text");
            e.Clear();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void TextAreaValueChange()
        {
            // TODO
        }

        [TearDown]
        public void TearDown()
        {
            ResultState resultState = TestContext.CurrentContext.Result.Outcome;

            if (resultState.Status == TestStatus.Passed)
            {
                logger.Debug("Test result: pass");
            }
            if (resultState.Status == TestStatus.Failed && resultState.Label != "Error")
            {
                logger.Fatal("Test result: fail - assertion fail");
                logger.Fatal(TestContext.CurrentContext.Result.Message);
                logger.Fatal(TestContext.CurrentContext.Result.StackTrace);
            }
            if (resultState.Status == TestStatus.Failed && resultState.Label == "Error")
            {
                logger.Fatal("Test result: fail - exception was thrown");
                logger.Fatal(TestContext.CurrentContext.Result.Message);
                logger.Fatal(TestContext.CurrentContext.Result.StackTrace);
            }

            string message = "End test: " + TestContext.CurrentContext.Test.FullName;
            logger.Debug(message);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            d.Quit();
        }
    }
}
