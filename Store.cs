using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QA_BandWebsite
{
    [TestClass]
    public class Store : Common
    {

        [TestMethod, Description("Verifies the loading of the store page - TC001")]
        [TestCategory("Regression")]
        public void LoadStorePage()
        {
            var a = GeneralControls.PageHeader();
            //Verify Page Header is Displayed
            Assert.IsTrue(GeneralControls.PageHeader() != null, "Did not find page header on store page");
            Assert.IsTrue(GeneralControls.PageHeader().Text.Equals("Dreamers"), $"Did not find expected text in page header Expected: Dreamers Actual: {GeneralControls.PageHeader().Text}");

            //Verify Music Section Header is Displayed
            Assert.IsTrue(StoreControls.MusicHeader() != null, "Did not find music section header on store page");
            Assert.IsTrue(StoreControls.MusicHeader().Text.Equals("MUSIC"), $"Did not find expected text in music section header Expected: MUSIC Actual: {StoreControls.MusicHeader().Text}");

            //Verify Merch Section Header is Displayed
            Assert.IsTrue(StoreControls.MerchHeader() != null, "Did not find merch section header on store page");
            Assert.IsTrue(StoreControls.MerchHeader().Text.Equals("MERCH"), $"Did not find expected text in merch section header Expected: MERCH Actual: {StoreControls.MerchHeader().Text}");

            //Verify Cart Section Header is Displayed
            Assert.IsTrue(StoreControls.CartHeader() != null, "Did not find cart section header on store page");
            Assert.IsTrue(StoreControls.CartHeader().Text.Equals("CART"), $"Did not find expected text in cart section header Expected: CART Actual: {StoreControls.CartHeader().Text}");

            //Verify Page Footer is Displayed
            Assert.IsTrue(GeneralControls.PageFooter() != null, "Did not find page footer on store page");
            Assert.IsTrue(GeneralControls.PageFooter().Text.Equals("Dreamers"), $"Did not find expected text in page footer Expected: Dreamers Actual: {GeneralControls.PageFooter().Text}");

        }


        [TestInitialize]
        public void TestIntialize()
        {
            //Tests Run against local instance of my site
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
                    //take screen-shot of failure
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

        public GeneralControls GeneralControls = new GeneralControls();
        public StoreControls StoreControls = new StoreControls();
    }
}

