using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace SimplePageFactory.Helpers
{
    public static class WaitHelper
    {
        /// <summary>
        /// Waiting up to <paramref name="timeout"/> seconds, polling every <paramref name="pollingInterval"/> 
        /// miliseconds and checking if <paramref name="condition"/> returns true.
        /// Otherwise throw exception.
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="timeout">Polling interval in seconds.</param>
        /// <param name="pollingInterval">Polling interval in miliseconds.</param>
        public static IWebElement WaitUntil(this IWebElement element, Func<IWebElement, bool> condition, 
            int timeout = 10, int pollingInterval = 100)
        {
            IWait<IWebElement> wait = new DefaultWait<IWebElement>(element)
            {
                Timeout = TimeSpan.FromSeconds(timeout),
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
            };
            wait.Until(condition);
            return element;
        }

        public static Func<IWebElement, bool> ElementNotMoving =
            (e) =>
            {
                try
                {
                    var location = e.Location;
                    var size = e.Size;
                    Thread.Sleep(100);
                    var newLocation = e.Location;
                    var newSize = e.Size;

                    if (location.Equals(newLocation) && size.Equals(newSize))
                        return true;
                    else
                        return false;
                }
                catch
                {
                    return false;
                }
            };

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
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        #region new file
        //public static bool WaitForTextPresentedInField(IWebDriver d, IWebElement e, string text, int time = 30)
        //{
        //    return new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(TextPresentedInField(e, text));
        //}

        //public static string WaitForSomeTextPresentedInField(IWebDriver d, IWebElement e, int time = 30)
        //{
        //    return new WebDriverWait(d, TimeSpan.FromSeconds(time)).Until(SomeTextPresentedInField(e));
        //}


        //[Test]
        //public void Test()
        //{
        //    var d = new ChromeDriver();
        //    d.Navigate().GoToUrl("data:text/html, <h1>Write 'elo' to first input and click button<h1/>" +
        //        " <input id='input1' value=''/><br/><button onclick='myFunction()'>Copy to second field</button> " +
        //        "<br/> <input id='input2' value='' disabled/> <script> function myFunction()" +
        //        " { document.getElementById('input2').value = document.getElementById('input1').value; } </script>");
        //    IWebElement e = d.FindElement(By.Id("input2"));
        //    WaitHelper.WaitForTextPresentedInField(d, e, "elo");
        //}
        #endregion

        #region new file
        //    IWebDriver d;
        //    Stopwatch sw = new Stopwatch();


        //    [Test]
        //    [TestCase("Chrome")]
        //    [TestCase("Firefox")]
        //    [TestCase("IE")]
        //    [TestCase("Edge")]
        //    public void ClickBySelenium(string browser)
        //    {
        //        d = Browser(browser);

        //        StartPage sp = new StartPage(d);
        //        sp.GoToClickPage();

        //        ClickPage cp = new ClickPage(d);
        //        cp.ClickButton();

        //        OnetPage op = new OnetPage(d);
        //    }

        //    [Test]
        //    [TestCase("Chrome")]
        //    [TestCase("Firefox")]
        //    [TestCase("IE")]
        //    [TestCase("Edge")]
        //    public void HoverBySelenium(string browser)
        //    {
        //        sw.Restart();
        //        d = Browser(browser);

        //        StartPage sp = new StartPage(d);
        //        sp.GoToHoverPage();

        //        HoverPage hp = new HoverPage(d);
        //        hp.HoverButton();

        //        OnetPage op = new OnetPage(d);
        //        sw.Stop();
        //        Console.WriteLine(sw.Elapsed.Seconds + " s");
        //    }


        //    [TearDown]
        //    public void TearDown()
        //    {
        //        d.Close();
        //    }

        //    private IWebDriver Browser(string browser)
        //    {
        //        if (browser == "Chrome")
        //            return new ChromeDriver();
        //        else if (browser == "Firefox")
        //            return new FirefoxDriver();
        //        else if (browser == "IE")
        //            return new InternetExplorerDriver();
        //        else if (browser == "Edge")
        //            return new EdgeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        //        else throw new NotImplementedException();
        //    }
        //}

        //public abstract class BasePage
        //{
        //    protected virtual string[] Url => new string[] { };
        //    protected IWebDriver driver;

        //    public BasePage(IWebDriver driver)
        //    {
        //        this.driver = driver;
        //        PageFactory.InitElements(driver, this);

        //        try
        //        {
        //            new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(
        //                (d) =>
        //                {
        //                    Console.WriteLine("trying hard");
        //                    return
        //                    ((string)((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")).Equals("complete") &&
        //                    (Url.Length > 0 ? Url.Any((url) => driver.Url.Contains(url)) : true);
        //                }
        //            );
        //        }
        //        catch (WebDriverTimeoutException ex)
        //        {
        //            throw new Exception($"Error on page: {driver.Url}, URL should contain any of: {string.Join(", ", Url)}", ex);
        //        }
        //    }
        //}

        //public class StartPage : BasePage
        //{

        //    public StartPage(IWebDriver driver) : base(driver)
        //    {
        //    }

        //    public void GoToClickPage()
        //    {
        //        driver.Navigate().GoToUrl("file:///C:/Users/tomas/Documents/Visual%20Studio%202015/Projects/Tomasz/SimplePageFactory/Sites/clickPage.html");
        //    }

        //    public void GoToHoverPage()
        //    {
        //        driver.Navigate().GoToUrl("file:///C:/Users/tomas/Documents/Visual%20Studio%202015/Projects/Tomasz/SimplePageFactory/Sites/hoverPage.html");
        //    }
        //}

        //public class ClickPage : BasePage
        //{
        //    protected override string[] Url => new string[] { };

        //    public ClickPage(IWebDriver driver) : base(driver)
        //    {
        //    }

        //    private IWebElement Button => driver.FindElement(By.Id("button"));

        //    public void ClickButton()
        //    {
        //        Button.Click();
        //    }
        //}

        //public class HoverPage : BasePage
        //{
        //    protected override string[] Url => new string[] { };

        //    public HoverPage(IWebDriver driver) : base(driver)
        //    {

        //    }

        //    private IWebElement Button => driver.FindElement(By.Id("button"));

        //    public void HoverButton()
        //    {
        //        new Actions(driver).MoveToElement(Button).Perform();
        //    }
        //}

        //public class OnetPage : BasePage
        //{
        //    protected override string[] Url => new string[] { "LoginSignIn", "DomesticTransfer" };

        //    public OnetPage(IWebDriver driver) : base(driver)
        //    {
        //        Console.WriteLine("onet page constructor");
        //        Console.WriteLine(driver.Url);
        //    }
        //}
        #endregion
    }
}
