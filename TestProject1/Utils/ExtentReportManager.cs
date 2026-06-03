using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System.IO;

namespace Test_Store_Automation.Utils
{
    internal static class ExtentReportManager
    {
        private static AventStack.ExtentReports.ExtentReports? _extent; // Marked as nullable to satisfy CS8618
        private static ExtentHtmlReporter? _htmlReporter; // Marked as nullable for consistency
        private static readonly object _lock = new();

        public static AventStack.ExtentReports.ExtentReports Instance
        {
            get
            {
                if (_extent == null)
                {
                    lock (_lock)
                    {
                        if (_extent == null)
                        {
                            var reportPath = Path.Combine(TestContext.CurrentContext.WorkDirectory, "ExtentReport.html");
                            _htmlReporter = new ExtentHtmlReporter(reportPath);
                            _extent = new AventStack.ExtentReports.ExtentReports();
                            _extent.AttachReporter(_htmlReporter);
                        }
                    }
                }
                return _extent;
            }
        }

        public static void Flush()
        {
            _extent?.Flush();
        }
    }
}