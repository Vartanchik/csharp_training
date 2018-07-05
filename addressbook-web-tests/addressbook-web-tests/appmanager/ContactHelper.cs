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

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath(".//*[@id='maintable']/tbody/tr/td[3]")).Count;
        }

        private List<ContactData> getCache = null;

        public List<ContactData> GetContactList()
        {
            if (getCache == null)
            {
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elementsFirstName = driver.FindElements(By.XPath(".//*[@id='maintable']/tbody/tr/td[3]"));
                ICollection<IWebElement> elementsLastName = driver.FindElements(By.XPath(".//*[@id='maintable']/tbody/tr/td[2]"));
                for (int i = 0; i < elementsFirstName.Count; i++)
                {
                    getCache.Add(new ContactData(elementsFirstName.ElementAt<IWebElement>(i).Text, elementsLastName.ElementAt<IWebElement>(i).Text));
                }
            }
            return new List<ContactData>(getCache);
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Modify(int p, ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(new ContactData("", ""));
            }

            manager.Navigator.OpenHomePage();
            SelectContact(p);
            EditContact();
            FillContactForm(contact);
            SubmitContactEdit();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(new ContactData("", ""));
            }

            manager.Navigator.OpenHomePage();
            SelectContact(p);
            RemveContact();
            ConfirmRemoveContact();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("middlename"), contact.Middlename);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("nickname"), contact.Nickname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.Telhome);
            Type(By.Name("mobile"), contact.Telmobile);
            Type(By.Name("work"), contact.Telwork);
            Type(By.Name("fax"), contact.Telfax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.Homepage);
            Type(By.Name("byear"), contact.Byear);
            Type(By.Name("ayear"), contact.Ayear);
            Type(By.Name("address2"), contact.Address2);
            Type(By.Name("phone2"), contact.Phone2);
            Type(By.Name("notes"), contact.Notes);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            getCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            //string stringIndex = index.ToString();
            //driver.FindElement(By.Id(stringIndex)).Click();
            return this;
        }

        public ContactHelper RemveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            getCache = null;
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
            getCache = null;
            return this;
        }
    }
}
