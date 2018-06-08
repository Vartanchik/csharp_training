using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitNewContactCreation();
            app.Contacts.FillContactForm(new ContactData("MyName", "MyLastname"));
            app.Groups.Submit();
            // ERROR: Caught exception [Error: Dom locators are not implemented yet!]
        }
    }
}
