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
            newData.Middlename = null;
            newData.Nickname = null;
            newData.Title = null;
            newData.Company = null;
            newData.Address = null;
            newData.Telhome = null;
            newData.Telmobile = null;
            newData.Telwork = null;
            newData.Telfax = null;
            newData.Email = null;
            newData.Email2 = null;
            newData.Email3 = null;
            newData.Homepage = null;
            newData.Byear = null;
            newData.Ayear = null;
            newData.Address2 = null;
            newData.Phone2 = null;
            newData.Notes = null;

            app.Contacts.Modify(32, newData);

        }

    }
}
