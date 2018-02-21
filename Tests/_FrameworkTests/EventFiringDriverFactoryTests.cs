using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using SimplePageFactory.Helpers;

namespace SimplePageFactory.Tests._FrameworkTests
{
	[TestFixture]
    class EventFiringDriverFactoryTests
    {
        EventFiringWebDriver ed;
        IWebDriver d;
        IWebElement e;
        Logger l;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            d = new ChromeDriver();
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
            d.Navigate().GoToUrl("data:text/html,<title>page 1</title>");
            d.Navigate().Back();
            ed.Navigate().Forward();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            d.Quit();
        }
    }
}
