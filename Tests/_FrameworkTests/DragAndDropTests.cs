using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SimplePageFactory.Helpers;
using System;

namespace SimplePageFactory.Tests._FrameworkTests
{
    [TestFixture]
    public class DragAndDropTests
    {
        IWebDriver d;
        IWebElement s, t;

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

        [Test]
        public void HTML5Selenium() // this dont work.
        {
            d.Navigate().GoToUrl(@"C:\Users\tomas\Documents\Visual Studio 2015\Projects\Tomasz\SimplePageFactory\Sites\html5dnd.html");
            s = d.FindElement(By.Id("drag1"));
            t = d.FindElement(By.Id("div1"));
            Assert.That(d.FindElements(By.XPath("//body/img")).Count, Is.EqualTo(1));
            new Actions(d).DragAndDrop(s, t).Perform();
            Assert.That(d.FindElements(By.XPath("//body/img")).Count, Is.EqualTo(0));
        }

        [Test]
        public void HTML5JsDragAndDrop() // this works.
        {
            d.Navigate().GoToUrl(@"C:\Users\tomas\Documents\Visual Studio 2015\Projects\Tomasz\SimplePageFactory\Sites\html5dnd.html");
            s = d.FindElement(By.Id("drag1"));
            t = d.FindElement(By.Id("div1"));
            Assert.That(d.FindElements(By.XPath("//body/img")).Count, Is.EqualTo(1));
            s.JsDragAndDrop(t);
            Assert.That(d.FindElements(By.XPath("//body/img")).Count, Is.EqualTo(0));
        }

        [Test]
        public void SimpleSelenium() // this works.
        {
            d.Navigate().GoToUrl(@"http://demo.guru99.com/test/drag_drop.html");
            d.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Console.WriteLine(d.Manage().Timeouts().ImplicitWait.Seconds);

            s = d.FindElement(By.XPath("//*[@id='credit2']/a"));
            t = d.FindElement(By.XPath("//*[@id='bank']/li"));
            Assert.That(t.Text, Does.Not.Contain("BANK"));

            new Actions(d).DragAndDrop(s, t).Perform();

            t = d.FindElement(By.XPath("//*[@id='bank']/li"));
            Assert.That(t.Text, Does.Contain("BANK"));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            d.Quit();
        }
    }
}