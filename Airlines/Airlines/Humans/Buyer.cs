using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Buyer : Person
    {
        string _address;
        public string Address { get=>_address; set => _address = value; }
        public Buyer(string _firstName, string _lastName, string _phone, string _mail, string Address)
         : base(_firstName, _lastName, _phone, _mail)
        {
            this.Address = Address;
        }
        public override string Info(string info)
        {
            return info switch
            {
                "firstName" => FirstName,
                "lastName" => LastName,
                "Address" => _address,
                "phone" => Phone,
                "mail" => Mail,
                _ => "",
            };
        }
    }
}
