using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddressBookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allTels;
        private string allEmails;

        public ContactData(string firstname, string lastname)
        {
            FirstName = firstname;
            LastName = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return FirstName.GetHashCode() + LastName.GetHashCode();
        }

        public override string ToString()
        {
            return "firstname=" + FirstName + " " + "lastname=" + LastName;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Object.ReferenceEquals(this.LastName, other.LastName) == true)
            {
                return FirstName.CompareTo(other.FirstName);
            }
            else { return LastName.CompareTo(other.LastName); }
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string TelHome { get; set; }

        public string TelMobile { get; set; }

        public string TelWork { get; set; }

        public string TelFax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string HomePage { get; set; }

        public string Byear { get; set; }

        public string Ayear { get; set; }

        public string Address2 { get; set; }

        public string Phone2 { get; set; }

        public string Notes { get; set; }

        public string AllEmeils
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3 + "\r\n").Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllTels
        {
            get
            {
                if (allTels != null)
                {
                    return allTels;
                }
                else
                {
                    return (CleanUp(TelHome) + CleanUp(TelMobile) + CleanUp(TelWork)).Trim();
                }

            }
            set
            {
                allTels = value;
            }
        }

        private string CleanUp(string tel)
        {
            if (tel == null || tel == "")
            {
                return "";
            }
            return Regex.Replace(tel, "[ -()]", "") + "\r\n";
        }
    }
}
