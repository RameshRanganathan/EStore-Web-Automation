using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;
using Test_Store_Automation.Utils;

namespace Test_Store_Automation.BDD.Hooks
{
    [Binding]
    public sealed class ReqnrollHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver driver = null!;
        private ExtentTest test = null!;

        public ReqnrollHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // Create driver
            driver = DriverFactory.CreateDriver();

            // Register driver in DI container so step definitions can inject it
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<IWebDriver>(driver);

            // Create and register ExtentTest
            test = ExtentReportManager.Instance.CreateTest(_scenarioContext.ScenarioInfo.Title);
            _scenarioContext.ScenarioContainer.RegisterInstanceAs<ExtentTest>(test);

            test.Info("Starting scenario: " + _scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // Resolve ExtentTest from DI container
            var test = _scenarioContext.ScenarioContainer.Resolve<ExtentTest>();

            if (_scenarioContext.TestError != null)
            {
                string screenshotPath = ScreenshotHelper.TakeScreenshot(driver, _scenarioContext.ScenarioInfo.Title);
                test.Fail("Scenario failed: " + _scenarioContext.TestError.Message)
                    .AddScreenCaptureFromPath(screenshotPath);
            }
            else
            {
                test.Pass("Scenario passed");
            }

            driver?.Quit();
            driver?.Dispose();
        }

        [AfterTestRun]
        public static void AfterTestRun() => ExtentReportManager.Flush();
    }
}