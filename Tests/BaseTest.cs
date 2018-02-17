using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Text;

namespace SimplePageFactory.Tests
{
    abstract class BaseTest
    {
        private IWebDriver _driver;
        protected IWebDriver Driver { get; private set; }

        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ExtentReports extent;
        protected ExtentTest htmlLogger;

        private static string resultsDir;

        [OneTimeSetUp]
        public void BaseOneTimeSetUp()
        {
            var browser = Browsers.Chrome;
            _driver = DriverFactory.Create(browser);
            Driver = _driver;

            //TODO: event firing driver
            //Driver = new EventFiringDriverFactory().Create(_driver, logger);

            // TODO: używanie TestDirectory w tym miejscu jest bez sensu
            var dir = TestContext.CurrentContext.TestDirectory;
            resultsDir = dir + @"\..\..\Results\";
            var fullClassName = TestContext.CurrentContext.Test.ClassName;
            var className = fullClassName.Substring(fullClassName.LastIndexOf(".") + 1);
            var reporter = new ExtentHtmlReporter(resultsDir + className + ".html");

            reporter.Configuration().Theme = Theme.Dark;
            reporter.Configuration().DocumentTitle = "My title";
            reporter.Configuration().ReportName = className + " - Report";
            reporter.Configuration().Protocol = Protocol.HTTPS;

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            var browserName = ((RemoteWebDriver)Driver).Capabilities.BrowserName;
            var browserVersion = ((RemoteWebDriver)Driver).Capabilities.Version;
            // TODO: osVErsion for win10 dont work, also is 32 or 64 
            var osVersion = Environment.OSVersion.ToString();

            //TODO: env, jira, user credentuaks hardcoded
            extent.AddSystemInfo("App environment", "UAT");
            extent.AddSystemInfo("Browser", $"{browserName} {browserVersion}");
            extent.AddSystemInfo("OS", osVersion);
            extent.AddSystemInfo("JIRA", @"<a href=""https://www.onet.pl"">MNTT-112</a>");
            extent.AddSystemInfo("User credentials", "123456 / 22222222 / 1234567890");
        }

        [SetUp]
        public void BaseSetUp()
        {
            string message = "Start test " + TestContext.CurrentContext.Test.FullName;
            TestContext.WriteLine(message);
            logger.Debug(message);

            htmlLogger = extent.CreateTest(TestContext.CurrentContext.Test.MethodName);
            htmlLogger.Info(MarkupHelper.CreateLabel("Start test", ExtentColor.Blue));
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

            htmlLogger.Info("End test");

            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : "<br/>Stack Trace:<br/><pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";

            var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? ""
                : "Error Message:<br/><pre>" + TestContext.CurrentContext.Result.Message + "</pre>";

            Status logStatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logStatus = Status.Fail;
                    htmlLogger.Fail (MarkupHelper.CreateLabel("FAIL", ExtentColor.Red));

                    var timeStamp = new StringBuilder(DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                    var filePath = resultsDir + $@"imgs\{timeStamp}.PNG";

                    //TODO: what if browser is closed?
                    var ss = ((ITakesScreenshot)Driver).GetScreenshot();
                    ss.SaveAsFile(filePath, ScreenshotImageFormat.Png);

                    htmlLogger.AddScreenCaptureFromPath(filePath);

                    break;
                case TestStatus.Passed:
                    logStatus = Status.Pass;
                    htmlLogger.Pass(MarkupHelper.CreateLabel("PASS", ExtentColor.Green));
                    break;
                default:
                    logStatus = Status.Warning;
                    break;
            }

            htmlLogger.Log(logStatus, errorMessage + stackTrace);
        }

        [OneTimeTearDown]
        public void BaseOneTimeTearDown()
        {
            extent.Flush();

            Driver.Quit();
        }
    }
}
