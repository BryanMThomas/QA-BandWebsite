using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QA_BandWebsite
{
    [TestClass]
    public class Store : Common
    {

        [TestMethod, Description("Verifies XYZ")]
        [TestCategory("Regression")]
        public void TestMethod1()
        {

            var a = 0;
        }


        [TestInitialize]
        public void TestIntialize()
        {
            //Tests Run against local instace of my site
            GetDriver("Chrome", "http://localhost:3000/store");

            //TODO Add Timer to tests to output in logs
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.CurrentTestOutcome.ToString().Equals("Failed"))
            {
                try
                {
                    var error = GetErrorFromTestContext(TestContext);
                    var errorMessage = error[0];
                    var stackTrace = error[1];

                    Console.WriteLine($"Error Message: {errorMessage} StackTrace: {stackTrace}");
                    //take screenshot of failure
                    CaptureScreenshot(TestContext.TestResultsDirectory + "\\" + TestContext.TestName + "_Failure.png");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception Caught in Test Cleanup Exception: " + e.ToString());
                }
            }

            driver.Quit();
        }

        public TestContext TestContext { get; set; }
    }
}

