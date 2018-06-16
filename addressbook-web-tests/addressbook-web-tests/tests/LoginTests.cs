using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressBookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentialsTest()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);

            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredentialsTest()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "12354");
            app.Auth.Login(account);

            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
