using OpenQA.Selenium;
using NUnit.Framework;
using System;
using System.IO;

namespace Test_Store_Automation.Utils
{
    internal static class ScreenshotHelper
    {
        public static string TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                var screenshotsDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
                if (!Directory.Exists(screenshotsDir))
                    Directory.CreateDirectory(screenshotsDir);

                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmssfff}.png";
                var filePath = Path.Combine(screenshotsDir, fileName);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(filePath);

                return filePath;
            }
            catch (Exception ex)
            {
                TestContext.WriteLine("Failed to capture screenshot: " + ex.Message);
                return null;
            }
        }
    }
}