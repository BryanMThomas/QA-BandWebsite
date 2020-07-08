using System;
using OpenQA.Selenium;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace QA_BandWebsite
{
    public class StoreControls : Common
    {
        #region Controls
        //Section Headers
        public IWebElement MusicHeader(int timeOut = 30) { return GetControl(driver, By.Id("music-header"), timeOut); }
        public IWebElement MerchHeader(int timeOut = 30) { return GetControl(driver, By.Id("merch-header"), timeOut); }
        public IWebElement CartHeader(int timeOut = 30) { return GetControl(driver, By.Id("cart-header"), timeOut); }
        #endregion

        #region Methods
        #endregion

    }
}
