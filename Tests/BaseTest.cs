﻿using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace SimplePageFactory.Tests
{
    abstract class BaseTest
    {
        protected EventFiringWebDriver Driver { get; private set; }
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            Driver = new EventFiringWebDriver(new ChromeDriver());
            Driver.ElementClicking += Driver_ElementClicking;
            Driver.ElementValueChanged += Driver_ElementValueChanged;
        }

        private void Driver_ElementValueChanged(object sender, WebElementEventArgs e)
        {
            logger.Debug($"On website: {e.Driver.Url, -50} changed value of element with tag name: '{e.Element.TagName}', actual value: '{e.Element.GetAttribute("value")}'");
        }

        private void Driver_ElementClicking(object sender, WebElementEventArgs e)
        {
            logger.Debug("Clicking on element with tag name: " + e.Element.TagName);
        }

        [SetUp]
        public void BaseSetUp()
        {
            string message = "Start test " + TestContext.CurrentContext.Test.FullName;
            TestContext.WriteLine(message);
            logger.Debug(message);
        }

        [TearDown]
        public void BaseTearDown()
        {
            logger.Debug("End test");
            // TODO : add logging on fail
            string message = "End test " + TestContext.CurrentContext.Test.FullName;
            ResultState resultState = TestContext.CurrentContext.Result.Outcome;

            if (resultState.Status == TestStatus.Passed)
            {
                TestContext.WriteLine("Test pass");
                //logger.Debug(message);
            }

            if (resultState.Status == TestStatus.Failed && resultState.Label != "Error")
            {
                TestContext.WriteLine("Test fail - assertion fail");
            }

            if (resultState.Status == TestStatus.Failed && resultState.Label == "Error")
            {
                TestContext.WriteLine("Test fail - unexpected exception");
            }
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            Driver.Quit();
        }
    }
}
