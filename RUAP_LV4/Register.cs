using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class Register
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        Random rnd = new Random(DateTime.Now.Millisecond);
        int mailNum;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demo.opencart.com/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheRegisterTest()
        {
            mailNum = rnd.Next(1000, 9999);

            driver.Navigate().GoToUrl(baseURL + "/");
            Thread.Sleep(5000);
            Assert.AreEqual("Your Store", driver.Title);
            driver.FindElement(By.LinkText("My Account")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Register")).Click();
            Assert.AreEqual("Register Account", driver.Title);
            driver.FindElement(By.Id("input-firstname")).Clear();
            driver.FindElement(By.Id("input-firstname")).SendKeys("milan");
            driver.FindElement(By.Id("input-lastname")).Clear();
            driver.FindElement(By.Id("input-lastname")).SendKeys("milan");
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys("my_email"+mailNum+"@mail.com");
            driver.FindElement(By.Id("input-telephone")).Clear();
            driver.FindElement(By.Id("input-telephone")).SendKeys("12345678");
            driver.FindElement(By.Id("input-address-1")).Clear();
            driver.FindElement(By.Id("input-address-1")).SendKeys("Kneza Trpimira 4B");
            driver.FindElement(By.Id("input-city")).Clear();
            driver.FindElement(By.Id("input-city")).SendKeys("Osijek");
            new SelectElement(driver.FindElement(By.Id("input-country"))).SelectByText("Croatia");
            Thread.Sleep(5000);
            new SelectElement(driver.FindElement(By.Id("input-zone"))).SelectByText("Osječko-baranjska");
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys("12345678");
            driver.FindElement(By.Id("input-confirm")).Clear();
            driver.FindElement(By.Id("input-confirm")).SendKeys("12345678");
            driver.FindElement(By.Name("agree")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            Assert.AreEqual("Your Account Has Been Created!", driver.Title);
            driver.FindElement(By.LinkText("Continue")).Click();
            Assert.AreEqual("My Account", driver.Title);
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
