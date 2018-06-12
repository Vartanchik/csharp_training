using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebPracriceTests
{
    public class ContactHelper : HelperBase
    {
        private bool acceptNextAlert;
        
        public ContactHelper(ApplicationManager manager, bool acceptNextAlert)
            : base(manager)
        {
            this.acceptNextAlert = acceptNextAlert;
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

        public ContactHelper Remove()
        {
            SelectContact();
            RemveContact();
            ConfirmRemoveContact();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Id("15")).Click();
            return this;
        }

        public ContactHelper RemveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ConfirmRemoveContact()
        {
            NUnit.Framework.Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }

        public bool IsElementPresent(By by)
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

        public bool IsAlertPresent()
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
    }
}
