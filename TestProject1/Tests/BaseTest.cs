using NUnit.Framework;
using OpenQA.Selenium;
using AventStack.ExtentReports;
using Test_Store_Automation.Reports;
using Test_Store_Automation.Utils;

namespace Test_Store_Automation.Tests
{
    public abstract class BaseTest
    {
        protected IWebDriver? driver;
        protected ExtentTest? test;

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentReportManager.Flush();
        }

        [TearDown]
        public void AfterEachTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMsg = TestContext.CurrentContext.Result.Message;

            if (test != null)
            {
                if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    if (driver != null)
                    {
                        string screenshotPath = ScreenshotHelper.TakeScreenshot(driver, TestContext.CurrentContext.Test.Name);
                        test.Fail("Test failed: " + errorMsg)
                            .AddScreenCaptureFromPath(screenshotPath);
                    }
                    else
                    {
                        test.Fail("Test failed: " + errorMsg + " (Screenshot not available as driver is null)");
                    }
                }
                else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
                {
                    test.Pass("Test passed");
                }
                else
                {
                    test.Skip("Test skipped");
                }
            }

            driver?.Quit();
            driver?.Dispose();
        }
    }
}