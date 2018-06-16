using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("MyName", "MyLastname");

            app.Contacts.Create(contact);
            // ERROR: Caught exception [Error: Dom locators are not implemented yet!]
        }
    }
}
