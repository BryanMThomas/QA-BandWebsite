using System;
using System.Threading;
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

        [TestMethod, Description("Verifies behavior for Adding Items to the Cart - TC004")]
        [TestCategory("Regression")]
        public void AddItemsCartStorePage()
        {
            #region First item
            // Add an item to the Cart 
            StoreControls.BtnShopItemAddToCart().Click();
            // Verify cart item added
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 1, $"Cart item not added after add to cart was selected. Expected Count: 1 Actual: {StoreControls.TxtCartItems().Count}");
            Assert.IsNotNull(StoreControls.TxtCartItemTitle(), "Did not find title for cart item on store page.");
            Assert.IsNotNull(StoreControls.ImgCartItemImage(), "Did not find image for cart item on store page.");
            Assert.IsNotNull(StoreControls.TxtCartItemPrice(), "Did not find price for cart item on store page.");
            Assert.IsNotNull(StoreControls.TxtCartItemQuantity(), "Did not find quantity for shop item on store page.");
            // Verify quantity
            Assert.IsTrue(StoreControls.TxtCartItemQuantity().GetAttribute("value").Equals("1"), $"Quantity was not as expected. Expected Count: 1 Actual: {StoreControls.TxtCartItemQuantity().Text}");
            // Verify total is increased
            var actualTotal = float.Parse(StoreControls.TxtCartTotalPrice().Text.Replace("$", ""));
            var expectedTotal = int.Parse(StoreControls.TxtCartItemQuantity().GetAttribute("value")) * float.Parse(StoreControls.TxtCartItemPrice().Text.Replace("$", ""));
            Assert.IsTrue(expectedTotal == actualTotal, $"Did not find expected total in cart after adding an item to the cart Expected: {expectedTotal} Actual: {actualTotal}");
            #endregion

            #region Second Item
            // Add another unique item to the cart
            StoreControls.BtnShopItemAddToCart(4).Click();
            // Verify cart item added
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 2, $"Cart item not added after add to cart was selected. Expected Count: 2 Actual: {StoreControls.TxtCartItems().Count}");
            Assert.IsNotNull(StoreControls.TxtCartItemTitle(1), "Did not find title for cart item on store page.");
            Assert.IsNotNull(StoreControls.ImgCartItemImage(1), "Did not find image for cart item on store page.");
            Assert.IsNotNull(StoreControls.TxtCartItemPrice(1), "Did not find price for cart item on store page.");
            Assert.IsNotNull(StoreControls.TxtCartItemQuantity(1), "Did not find quantity for shop item on store page.");
            // Verify quantity
            Assert.IsTrue(StoreControls.TxtCartItemQuantity(1).GetAttribute("value").Equals("1"), $"Quantity was not as expected. Expected Count: 1 Actual: {StoreControls.TxtCartItemQuantity(1).Text}");
            // Verify total is increased
            actualTotal = float.Parse(StoreControls.TxtCartTotalPrice().Text.Replace("$", ""));
            expectedTotal = expectedTotal + int.Parse(StoreControls.TxtCartItemQuantity(1).GetAttribute("value")) * float.Parse(StoreControls.TxtCartItemPrice(1).Text.Replace("$", ""));
            Assert.IsTrue(expectedTotal == actualTotal, $"Did not find expected total in cart after adding a second item to the cart Expected: {expectedTotal} Actual: {actualTotal}");
            #endregion

            #region Duplicate Item
            // Add another item that is already in the cart
            StoreControls.BtnShopItemAddToCart().Click();
            // Verify new cart item is not added
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 2, $"Additional cart item added after adding an item that was already in the cart. Expected Count: 2 Actual: {StoreControls.TxtCartItems().Count}");
            // Verify quantity
            Assert.IsTrue(StoreControls.TxtCartItemQuantity().GetAttribute("value").Equals("2"), $"Quantity was not as expected. Expected Count: 2 Actual: {StoreControls.TxtCartItemQuantity().Text}");
            // Verify total is increased
            actualTotal = float.Parse(StoreControls.TxtCartTotalPrice().Text.Replace("$", ""));
            var item1Total = int.Parse(StoreControls.TxtCartItemQuantity().GetAttribute("value")) * float.Parse(StoreControls.TxtCartItemPrice().Text.Replace("$", ""));
            var item2Total = int.Parse(StoreControls.TxtCartItemQuantity(1).GetAttribute("value")) * float.Parse(StoreControls.TxtCartItemPrice(1).Text.Replace("$", ""));
            expectedTotal = item1Total + item2Total;
            Assert.IsTrue(expectedTotal == actualTotal, $"Did not find expected total in cart after adding a second item to the cart Expected: {expectedTotal} Actual: {actualTotal}");
            #endregion
        }

        [TestMethod, Description("Verifies behavior for Removing Items to the Cart - TC005")]
        [TestCategory("Regression")]
        public void RemoveItemCartStorePage()
        {
            #region Test Setup
            //Add 2 of the same item to the cart  
            StoreControls.BtnShopItemAddToCart().Click();
            StoreControls.BtnShopItemAddToCart().Click();
            //Add 1 of a different item to the cart
            StoreControls.BtnShopItemAddToCart(2).Click();
            //Verify items added to the cart
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 2, $"Additional cart item added after adding an item that was already in the cart. Expected Count: 2 Actual: {StoreControls.TxtCartItems().Count}");
            #endregion 

            #region First Item
            //Verify Remove Button is displayed for first item
            Assert.IsNotNull(StoreControls.BtnCartItemRemove(), $"Remove button not found for first cart item");
            //Verify Remove Button is displayed for second item 
            Assert.IsNotNull(StoreControls.BtnCartItemRemove(1), $"Remove button not found for second cart item");
            //Click Remove for first item and verify new quantity
            StoreControls.BtnCartItemRemove().Click();
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 1, $"Cart item not removed after remove was selected. Expected Count: 1 Actual: {StoreControls.TxtCartItems().Count}");
            //Verify total is updated 
            var actualTotal = float.Parse(StoreControls.TxtCartTotalPrice().Text.Replace("$", ""));
            var expectedTotal = int.Parse(StoreControls.TxtCartItemQuantity(1).GetAttribute("value")) * float.Parse(StoreControls.TxtCartItemPrice(1).Text.Replace("$", ""));
            Assert.IsTrue(expectedTotal == actualTotal, $"Did not find expected total in cart after removing from the cart Expected: {expectedTotal} Actual: {actualTotal}");
            #endregion

            #region Second Item
            //Click Remove for second item and verify new quantity
            StoreControls.BtnCartItemRemove(1).Click();
            Assert.IsTrue(StoreControls.TxtCartItems().Count == 0, $"Cart item not removed after remove was selected. Expected Count: 0 Actual: {StoreControls.TxtCartItems().Count}");
            //Verify Empty Cart
            Assert.IsNotNull(StoreControls.TxtEmptyCart(), "Cart was not empty after removing all items from the cart");
            #endregion

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

