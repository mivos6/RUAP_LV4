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
    public class LoginBuyCheckout
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
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
        public void TheLoginBuyCheckoutTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            Thread.Sleep(6000);
            Assert.AreEqual("Your Store", driver.Title);
            driver.FindElement(By.LinkText("My Account")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("Account Login", driver.Title);
            driver.FindElement(By.Id("input-email")).Clear();
            driver.FindElement(By.Id("input-email")).SendKeys("my_email1111@mail.com");
            driver.FindElement(By.Id("input-password")).Clear();
            driver.FindElement(By.Id("input-password")).SendKeys("12345678");
            driver.FindElement(By.CssSelector("input.btn.btn-primary")).Click();
            Assert.AreEqual("My Account", driver.Title);
            driver.FindElement(By.LinkText("Desktops")).Click();
            driver.FindElement(By.LinkText("Mac (1)")).Click();
            Assert.AreEqual("", driver.Title);
            driver.FindElement(By.CssSelector("div.image > a > img.img-responsive")).Click();
            Assert.AreEqual("", driver.Title);
            driver.FindElement(By.Id("button-cart")).Click();
            driver.FindElement(By.XPath("(//button[@type='button'])[5]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[@id='cart']/ul/li[2]/div/p/a[2]/strong")).Click();
            Thread.Sleep(1000);
            Assert.AreEqual("Shopping Cart", driver.Title);
            driver.FindElement(By.CssSelector("a.btn.btn-primary")).Click();
            Assert.AreEqual("Shopping Cart", driver.Title);
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("My Account")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.LinkText("Logout")).Click();
            Assert.AreEqual("Account Logout", driver.Title);
            driver.FindElement(By.LinkText("Continue")).Click();
            Assert.AreEqual("Your Store", driver.Title);
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
