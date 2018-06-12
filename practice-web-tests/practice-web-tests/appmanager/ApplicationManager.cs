using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebPracriceTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;
        protected bool acceptNextAlert = true;

        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected ContactHelper contactHelper;

        public ApplicationManager()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"c:\Program Files\Mozilla Firefox\firefox.exe";
            options.UseLegacyImplementation = true;
            driver = new FirefoxDriver(options);
            baseURL = "http://localhost";
            verificationErrors = new StringBuilder();

            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            contactHelper = new ContactHelper(this, acceptNextAlert);
        }

        public bool AcceptNextAlert
        {
            get
            {
                return acceptNextAlert;
            }
        }

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            NUnit.Framework.Assert.AreEqual("", verificationErrors.ToString());
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigator
        {
            get
            {
                return navigationHelper;
            }
        }

        public ContactHelper Contact
        {
            get
            {
                return contactHelper;
            }
        }
    }
}
