using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("jjj");
            newData.Footer = "sss";
            newData.Header = "aaa";

            app.Groups.Modify(1, newData);

        }
    }
}
