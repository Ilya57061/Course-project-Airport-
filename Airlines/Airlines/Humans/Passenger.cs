using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airlines
{
    public class Passenger : Person
    {
        string _docNumber;
        public string DocNumber { get => _docNumber; set => _docNumber = value; }
        public Passenger(string _firstName, string _lastName, string _phone, string _mail, string DocNumber)
          : base(_firstName, _lastName, _phone, _mail)
        {
            this.DocNumber = DocNumber;
        }
        public override string Info(string info)
        {
            return info switch
            {
                "firstName" => FirstName,
                "lastName" => LastName,
                "Doc" => _docNumber,
                "phone" => Phone,
                "mail" => Mail,
                _ => "",
            };
        }
    }
}
