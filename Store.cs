using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace QA_BandWebsite
{
    [TestClass]
    public class Store : Common
    {

        [TestMethod, Description("Verifies the loading of the store page - TC001")]
        [TestCategory("Regression")]
        public void LoadStorePage()
        {
            //Verify Page Header is Displayed
            Assert.IsNotNull(GeneralControls.PageHeader(), "Did not find page header on store page");
            Assert.IsTrue(GeneralControls.PageHeader().Text.Equals("Dreamers"), $"Did not find expected text in page header Expected: Dreamers Actual: {GeneralControls.PageHeader().Text}");

            //Verify Music Section Header is Displayed
            Assert.IsNotNull(StoreControls.TxtMusicHeader(), "Did not find music section header on store page");
            Assert.IsTrue(StoreControls.TxtMusicHeader().Text.Equals("MUSIC"), $"Did not find expected text in music section header Expected: MUSIC Actual: {StoreControls.TxtMusicHeader().Text}");

            //Verify Merch Section Header is Displayed
            Assert.IsNotNull(StoreControls.TxtMerchHeader(), "Did not find merch section header on store page");
            Assert.IsTrue(StoreControls.TxtMerchHeader().Text.Equals("MERCH"), $"Did not find expected text in merch section header Expected: MERCH Actual: {StoreControls.TxtMerchHeader().Text}");

            //Verify Cart Section Header is Displayed
            Assert.IsNotNull(StoreControls.TxtCartHeader(), "Did not find cart section header on store page");
            Assert.IsTrue(StoreControls.TxtCartHeader().Text.Equals("CART"), $"Did not find expected text in cart section header Expected: CART Actual: {StoreControls.TxtCartHeader().Text}");

            //Verify Page Footer is Displayed
            Assert.IsNotNull(GeneralControls.PageFooter(), "Did not find page footer on store page");
            Assert.IsTrue(GeneralControls.PageFooter().Text.Equals("Dreamers"), $"Did not find expected text in page footer Expected: Dreamers Actual: {GeneralControls.PageFooter().Text}");

        }

        [TestMethod, Description("Verifies shop items on the store page - TC002")]
        [TestCategory("Regression")]
        public void ShopItemsStorePage()
        {
            for (int shopItemIterator = 1; shopItemIterator <= StoreControls.LstShopItems().Count; shopItemIterator++)
            {
                Assert.IsNotNull(StoreControls.TxtShopItemTitle(shopItemIterator), $"Did not find title for shop item on store page. Item Count {shopItemIterator}");
                Assert.IsNotNull(StoreControls.ImgShopItemImage(shopItemIterator), $"Did not find image for shop item on store page. Item Count {shopItemIterator}");
                Assert.IsNotNull(StoreControls.TxtShopItemPrice(shopItemIterator), $"Did not find price for shop item on store page. Item Count {shopItemIterator}");
                Assert.IsNotNull(StoreControls.BtnShopItemAddToCart(shopItemIterator), $"Did not find add to cart button for shop item on store page. Item Count {shopItemIterator}");
            }
        }

        [TestMethod, Description("Verifies behavior for Adding Items to the Cart - TC003")]
        [TestCategory("Regression")]
        public void EmptyCartStorePage()
        {
            //Verify Cart is Empty
            Assert.IsNotNull(StoreControls.TxtEmptyCart(), "Cart was not empty when launching store page for the first time");
            //Verify Empty Cart Text
            Assert.IsTrue(StoreControls.TxtEmptyCart().Text.Equals("Your Cart Is Empty"), $"Did not find expected text in empty cart. Expected: Your Cart Is Empty Actual: {StoreControls.TxtEmptyCart().Text}");
            //Verify No Total or Purchase Button is displayed
            Assert.IsNull(StoreControls.TxtCartTotal(2), "Found cart total when cart was empty");
            Assert.IsNull(StoreControls.BtnPurchase(2), "Found purchase button when cart was empty");
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

