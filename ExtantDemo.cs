using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;

namespace ExtantDemo
{
    [TestFixture]
    public class MyClassName
    {
        public ExtentReports extent;
        public ExtentTest htmlLogger;
        string fileName;

        [OneTimeSetUp]
        public void StartReport()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var fullClassName = TestContext.CurrentContext.Test.ClassName;
            var className = fullClassName.Substring(fullClassName.LastIndexOf(".") + 1);
            fileName = className + ".html";
            var reporter = new ExtentHtmlReporter(dir + fileName);

            reporter.Configuration().Theme = Theme.Dark;
            reporter.Configuration().DocumentTitle = "My title";
            reporter.Configuration().ReportName = className + " - Report";
            reporter.Configuration().Protocol = Protocol.HTTPS;

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            extent.AddSystemInfo("Env", "UAT");
            extent.AddSystemInfo("Browser", "Chrome");
            extent.AddSystemInfo("OS", "Win10");
            extent.AddSystemInfo("Jira key", "MNTT-997");
            extent.AddSystemInfo("Username", "123456 / 22222222 / 1234567890");
        }

        [SetUp]
        public void SetUp()
        {
            htmlLogger = extent.CreateTest(TestContext.CurrentContext.Test.MethodName);
            htmlLogger.Info("Start test");
        }


        [Test]
        public void MyTest1()
        {
            htmlLogger.Info("1. Navigate to login page");
            htmlLogger.Info("2. Input username and password");
            htmlLogger.Info("3. Click login button");
            htmlLogger.Info("4. Verify page title");
            Assert.That(true);
        }

        [Test]
        public void MyTest2()
        {
            string text = "some label";
            var m = MarkupHelper.CreateLabel(text, ExtentColor.Green);

            htmlLogger.Pass(m);

            Assert.That(true);
        }

        [Test]
        public void MyTest3()
        {
            Assert.That(false);
        }

        [Test]
        public void MyTest4()
        {
            htmlLogger.AssignAuthor("Tomasz Solarz");

            try
            {
                int a = 10;
                int b = 0;
                int c = a / b;
            }
            catch(Exception ex)
            {
                htmlLogger.Fail(ex);
            }
        }

        [TearDown]
        public void GetResult()
        {
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
                    break;
                case TestStatus.Passed:
                    logStatus = Status.Pass;
                    break;
                default:
                    logStatus = Status.Warning;
                    break;
            }



            htmlLogger.Log(logStatus, errorMessage + stackTrace);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }
    }
}
