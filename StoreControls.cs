using System;
using OpenQA.Selenium;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using System.Data;

namespace QA_BandWebsite
{
    public class StoreControls : Common
    {
        #region Controls
        //Section Headers
        public IWebElement TxtMusicHeader(int timeOut = 30) { return GetControl(driver, By.Id("music-header"), timeOut); }
        public IWebElement TxtMerchHeader(int timeOut = 30) { return GetControl(driver, By.Id("merch-header"), timeOut); }
        public IWebElement TxtCartHeader(int timeOut = 30) { return GetControl(driver, By.Id("cart-header"), timeOut); }

        //Shop Items
        public IList<IWebElement> LstShopItems(int timeOut = 30) { return driver.FindElements(By.ClassName("shop-item")); }
        public IWebElement TxtShopItemTitle(int index = 0, int timeOut = 30) { return GetControl(driver, By.XPath("//*[contains(@id,'shop-item-title-" + index + "')]"), timeOut); }
        public IWebElement ImgShopItemImage(int index = 0, int timeOut = 30) { return GetControl(driver, By.XPath("//*[contains(@id,'shop-item-img-" + index + "')]"), timeOut); }
        public IWebElement TxtShopItemPrice(int index = 0, int timeOut = 30) { return GetControl(driver, By.XPath("//*[contains(@id,'shop-item-price-" + index + "')]"), timeOut); }
        public IWebElement BtnShopItemAddToCart(int index = 0, int timeOut = 30) { return GetControl(driver, By.XPath("//*[contains(@id,'shop-item-button-" + index + "')]"), timeOut); }

        //Cart
        public IWebElement TxtEmptyCart(int timeOut = 30) { return GetControl(driver, By.Id("empty-cart"), timeOut); }
        public IWebElement TxtCartTotal(int timeOut = 30) { return GetControl(driver, By.Id("cart-total"), timeOut); }
        public IWebElement TxtCartTotalPrice(int timeOut = 30) { return GetControl(driver, By.Id("cart-total-price"), timeOut); }
        public IWebElement BtnPurchase(int timeOut = 30) { return GetControl(driver, By.ClassName("button-purchase"), timeOut); }

        #endregion

        #region Methods
        #endregion

    }
}
