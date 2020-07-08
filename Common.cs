using System;
using OpenQA.Selenium;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.DevTools.Network;

namespace QA_BandWebsite
{
    public class Common
    {
        public static IWebDriver driver;
        public static int activeBrowserPID;

        public string GetAppSetting(string key) { return ConfigurationManager.AppSettings.Get(key); }


        public IWebDriver GetDriver(string browser, string url = "about:blank", bool incognito = false)
        {

            switch (browser)
            {
                case "Firefox":
                    FirefoxOptions fireFoxOptions = new FirefoxOptions();
                    fireFoxOptions.Profile = new FirefoxProfile();
                    if (incognito)
                    {
                        fireFoxOptions.Profile.SetPreference("browser.private.browsing.autostart", true);
                    }
                    driver = new FirefoxDriver(fireFoxOptions);
                    break;
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    if (incognito)
                    {
                        chromeOptions.AddArgument("--incgonito");
                    }
                    driver = new ChromeDriver(chromeOptions);
                    break;
                default:
                    return null;

            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
            return driver;
        }

        public static List<string> GetErrorFromTestContext(TestContext testContext)
        {
            System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy;
            var field = testContext.GetType().GetField("m_currentResult", flags);
            var currentResult = field.GetValue(testContext);
            field = currentResult.GetType().GetField("m_errorInfo", flags);
            var errorInfo = field.GetValue(currentResult);
            field = errorInfo.GetType().GetField("m_message", flags);
            var message = field.GetValue(errorInfo);
            field = errorInfo.GetType().GetField("m_stackTrace", flags);
            var stackTrace = field.GetValue(message);
            var errorData = new List<string>();
            errorData.Add(message as string);
            errorData.Add(stackTrace as string);
            return errorData;
        }

        public void CaptureScreenshot(string path)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
        }

        public IWebElement GetControl(IWebDriver driver, By byStatement, int timeOut = 30)
        {
            IWebElement webElement = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut));
                webElement = wait.Until(d =>
                {
                    for (int attempts = 0; attempts < 3; attempts++)
                    {

                        try
                        {
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(byStatement));
                            webElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(byStatement));
                            return webElement;
                        }
                        catch (StaleElementReferenceException)
                        {
                            Thread.Sleep(500);
                        }
                    }
                    return null;
                });
            }
            catch (WebDriverTimeoutException) { }
            return webElement;
        }
    }
}
