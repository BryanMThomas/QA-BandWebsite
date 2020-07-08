using System;
using OpenQA.Selenium;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;

namespace QA_BandWebsite
{
    public class GeneralControls : Common
    {
        #region Controls
        public IWebElement PageHeader(int timeOut = 30) { return GetControl(driver, By.ClassName("band-name-large"), timeOut); }
        public IWebElement PageFooter(int timeOut = 30) { return GetControl(driver, By.Id("footer"), timeOut); }
        #endregion

        #region Methods
        #endregion

    }
}
