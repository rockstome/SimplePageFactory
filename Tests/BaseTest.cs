using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;

namespace SimplePageFactory.Tests
{
    abstract class BaseTest
    {
        private IWebDriver _driver;
        public IWebDriver Driver { get; private set; }
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();
        
        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            _driver = DriverFactory.Create(Browsers.Chrome);
            Driver = _driver;
            //Driver = new EventFiringDriverFactory().Create(_driver, logger);
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
