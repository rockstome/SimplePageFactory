using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace MainPage.Dashboard
{
    public static class Driver
    {
        private static IWebDriver driver = new ChromeDriver();

        public static void Navigate(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static T GenerateElement<T>(By by) where T : ControlBase
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { by });
        }

        public static Element FindElement(By by)
        {
            return new Element(driver.FindElement(by));
        }
    }

    public class Element
    {
        private IWebElement element;

        public IWebElement GetWrappedElement()
        {
            return element;
        }

        public Element(IWebElement element)
        {
            this.element = element;
        }

        public Element(By by)
        {
            element = Driver.FindElement(by).GetWrappedElement();
        }

        public Element FindElement(By by)
        {
            return new Element(element.FindElement(by));
        }

        public void Click()
        {
            element.Click();
        }

        internal void Clear()
        {
            element.Clear();
        }

        internal void SendKeys(string text)
        {
            element.SendKeys(text);
        }
    }

    public abstract class ControlBase
    {
        public Element element;

        public T GenerateElement<T>(By by) where T : ControlBase
        {
            return (T)Activator.CreateInstance(
                typeof(T), 
                new object[] { element.FindElement(by) });
        }

        public ControlBase(By by)
        {
        }
    }

    public class Field : ControlBase
    {
        public bool ValueEmpty { get { return false; } }

        public Field(By by) : base(by)
        {
        }

        public void Clear()
        {
            element.Clear();
        }

        public void Type(string text)
        {
            element.Click();
            element.Clear();
            element.SendKeys(text);
        }
    }

    public class Button : ControlBase
    {
        public Button(By by) : base(by)
        {
        }

        public void Click()
        {
            element.Click();
        }
    }

    public class Form : ControlBase
    {
        public Form(By by) : base(by)
        {
        }
    }

    public class LoginForm : Form
    {
        public LoginForm(By by) : base(by)
        {
        }

        public Field UsernameField => GenerateElement<Field>(By.Name("user"));
        public Field PassField => GenerateElement<Field>(By.Name("pass"));
        public Button SubmitButton => GenerateElement<Button>(By.Name("button"));

        /// <summary>
        /// Type username, password and click submit button.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void DoLogin(string username, string password)
        {
            UsernameField.Type(username);
            PassField.Type(password);
            SubmitButton.Click();
        }
    }

    public class InboxPage
    {
    }

    public class HomePage
    {
        public HomePage()
        {
        }

        public Button ExitButton => Driver.GenerateElement<Button>(By.Name(""));
        public Field SearchField => Driver.GenerateElement<Field>(By.Name(""));

        public LoginForm LoginForm => Driver.GenerateElement<LoginForm>(By.Name(""));

        /// <summary>
        /// Type username, password and click submit button. 
        /// If success you will be redirected to <see cref="InboxPage"/>
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public void LoginToApp(string username, string password)
        {
            LoginForm.UsernameField.Type(username);
            LoginForm.PassField.Type(password);
            LoginForm.SubmitButton.Click();
        }
    }

    public class TestClass
    {
        [Test]
        public void Test()
        {
            HomePage homePage = new HomePage();

            // simple action on elements
            homePage.ExitButton.Click();
            homePage.SearchField.Type("foo");
            homePage.SearchField.Clear();
            Assert.That(homePage.SearchField.ValueEmpty, Is.EqualTo(true));

            // #1
            homePage.LoginForm.DoLogin("foo", "bar");

            // #2
            homePage.LoginToApp("foo", "bar");
        }
    }
}
