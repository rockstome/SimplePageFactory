using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ExtantDemo
{
    [TestFixture]
    public class ExtantDemo
    {
        public ExtentReports extent;
        public ExtentTest test;

        [OneTimeSetUp]
        public void StartReport()
        {
            var dir = TestContext.CurrentContext.TestDirectory + "\\";
            var fileName = this.GetType().ToString() + ".html";
            var reporter = new ExtentHtmlReporter(dir + fileName);
            reporter.Configuration().Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            extent = new ExtentReports();
            extent.AttachReporter(reporter);

            extent.AddSystemInfo("Environment", "UAT");
            extent.AddSystemInfo("Jira key", "MNTT-997");
            extent.AddSystemInfo("Username", "123456 / 22222222 / 1234567890");
        }

        [Test]
        public void Test1()
        {
            test = extent.CreateTest("My first test");
            Assert.That(true);
        }

        [Test]
        public void Test2()
        {
            test = extent.CreateTest("My second test");
            Assert.That(false);
        }

        [TearDown]
        public void GetResult()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                ? ""
                : TestContext.CurrentContext.Result.StackTrace;

            var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? ""
                : TestContext.CurrentContext.Result.Message;

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

            test.Log(logStatus, errorMessage + stackTrace);
        }

        [OneTimeTearDown]
        public void EndReport()
        {
            extent.Flush();
        }
    }
}
