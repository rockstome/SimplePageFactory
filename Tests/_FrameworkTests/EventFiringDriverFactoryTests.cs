using NLog;
using NUnit.Framework;
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

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            d = new ChromeDriver();
            d.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            l = LogManager.GetCurrentClassLogger();
            ed = new EventFiringDriverFactory().CreateInstance(d, l);
        }
		
		[SetUp]
        public void SetUp()
        {
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
            AssertException();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Exception2()
        {
            SimpleException();
        }

        [Test, Category("EventFiringDriverFactoryTests")]
        public void Exception3()
        {
            InnerException();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            //d.Quit();
        }

        private void AssertException()
        {
            Assert.That(false);
        }

        private void SimpleException()
        {
            throw new Exception();
        }

        private void InnerException()
        {
            throw new FormatException("outer", new IndexOutOfRangeException("inner"));
        }
    }
}
