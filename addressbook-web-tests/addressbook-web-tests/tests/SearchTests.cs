using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void SearchTest()
        {
            int countNumber = app.Contacts.GetNumberOfSearchResults("p");
            int trNumber = app.Contacts.GetNumberOfContactsSearch();

            Assert.AreEqual(countNumber, trNumber);
        }
    }
}
