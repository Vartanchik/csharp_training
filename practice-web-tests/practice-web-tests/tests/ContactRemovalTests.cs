using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebPracriceTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contact.Remove();
        }
    }
}
