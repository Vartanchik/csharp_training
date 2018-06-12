using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressBookTests
{
    public class ContactHelper : HelperBase
    {
        private bool acceptNextAlert = true;

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

        public ContactHelper Create(ContactData contact)
        {
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(int p, ContactData contact)
        {
            SelectContact(p);
            EditContact();
            FillContactForm(contact);
            SubmitContactEdit();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            RemveContact();
            ConfirmRemoveContact();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.Firstname);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(contact.Middlename);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.Lastname);
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys(contact.Nickname);
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys(contact.Title);
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys(contact.Company);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address);
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys(contact.Telhome);
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(contact.Telmobile);
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys(contact.Telwork);
            driver.FindElement(By.Name("fax")).Clear();
            driver.FindElement(By.Name("fax")).SendKeys(contact.Telfax);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(contact.Email);
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys(contact.Email2);
            driver.FindElement(By.Name("email3")).Clear();
            driver.FindElement(By.Name("email3")).SendKeys(contact.Email3);
            driver.FindElement(By.Name("homepage")).Clear();
            driver.FindElement(By.Name("homepage")).SendKeys(contact.Homepage);
            driver.FindElement(By.Name("byear")).Clear();
            driver.FindElement(By.Name("byear")).SendKeys(contact.Byear);
            driver.FindElement(By.Name("ayear")).Clear();
            driver.FindElement(By.Name("ayear")).SendKeys(contact.Ayear);
            driver.FindElement(By.Name("address2")).Clear();
            driver.FindElement(By.Name("address2")).SendKeys(contact.Address2);
            driver.FindElement(By.Name("phone2")).Clear();
            driver.FindElement(By.Name("phone2")).SendKeys(contact.Phone2);
            driver.FindElement(By.Name("notes")).Clear();
            driver.FindElement(By.Name("notes")).SendKeys(contact.Notes);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            string stringIndex = index.ToString();
            //driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            driver.FindElement(By.Id(stringIndex)).Click();
            return this;
        }

        public ContactHelper RemveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ConfirmRemoveContact()
        {
            //driver.SwitchTo().Alert().Accept();
            NUnit.Framework.Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }

        public ContactHelper EditContact()
        {
            driver.FindElement(By.CssSelector("img[alt=\"Edit\"]")).Click();
            return this;
        }

        public ContactHelper SubmitContactEdit()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }
    }
}
