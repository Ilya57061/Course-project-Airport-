using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public abstract class Person
    {
        string _firstName;
        string _lastName;
        string _phone;
        string _mail;
        public string FirstName { get=>_firstName; set => _firstName = value;  }
        public string LastName { get=>_lastName; set => _lastName = value; }
        public string Phone { get=>_phone; set => _phone = value; }
        public string Mail { get=>_mail; set => _mail = value; }
        public Person(string FirstName, string LastName, string Phone, string Mail)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Phone = Phone;
            this.Mail = Mail;
        }
        public abstract string Info(string info);
    }
}
