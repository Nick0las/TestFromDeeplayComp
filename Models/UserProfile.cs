using System;

namespace TestFromDeeplayComp.Models
{
    internal class UserProfile
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
