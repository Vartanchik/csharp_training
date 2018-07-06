using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

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

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            EditContact(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string telHome = driver.FindElement(By.Name("home")).GetAttribute("value");
            string telMobile = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string telWork = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                TelHome =telHome,
                TelMobile = telMobile,
                TelWork = telWork,
            };
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allTels = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmeils = allEmails,
                AllTels = allTels,
            };
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
                getCache = new List<ContactData>();
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

        public ContactHelper Modify(int index, ContactData contact)
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(new ContactData("", ""));
            }

            manager.Navigator.OpenHomePage();
            //SelectContact(index);
            EditContact(index);
            FillContactForm(contact);
            SubmitContactEdit();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.Name("selected[]")))
            {
                Create(new ContactData("", ""));
            }

            manager.Navigator.OpenHomePage();
            SelectContact(index);
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
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("middlename"), contact.MiddleName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("nickname"), contact.NickName);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.TelHome);
            Type(By.Name("mobile"), contact.TelMobile);
            Type(By.Name("work"), contact.TelWork);
            Type(By.Name("fax"), contact.TelFax);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            Type(By.Name("homepage"), contact.HomePage);
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
        
        public ContactHelper EditContact(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactEdit()
        {
            driver.FindElement(By.Name("update")).Click();
            getCache = null;
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
