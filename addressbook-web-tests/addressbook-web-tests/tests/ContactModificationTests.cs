using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("NewMyName", "NewMyLastname");
            newData.MiddleName = null;
            newData.NickName = null;
            newData.Title = null;
            newData.Company = null;
            newData.Address = null;
            newData.TelHome = null;
            newData.TelMobile = null;
            newData.TelWork = null;
            newData.TelFax = null;
            newData.Email = null;
            newData.Email2 = null;
            newData.Email3 = null;
            newData.HomePage = null;
            newData.Byear = null;
            newData.Ayear = null;
            newData.Address2 = null;
            newData.Phone2 = null;
            newData.Notes = null;

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Modify(0, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].LastName = newData.LastName;
            oldContacts[0].FirstName = newData.FirstName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

        }

    }
}
