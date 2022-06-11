using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class PasswordPolicies : IPasswordPolicies
    {
        private Configuration _configuration = new Configuration();
        public int MinLength 
        { 
            get { return _configuration.GetValue<int>("passwordMinLength"); } 
            set { } 
        }
        public int MinSpecialCharacters
        {
            get { return _configuration.GetValue<int>("passwordMinSpecialCharacters"); }
            set { }
        }
        public int MinUppercase
        {
            get { return _configuration.GetValue<int>("passwordMinUppercase"); }
            set { }
        }
        public int MinLowercase
        {
            get { return _configuration.GetValue<int>("passwordMinLowercase"); }
            set { }
        }
        public int MinNumbers
        {
            get { return _configuration.GetValue<int>("passwordMinNumbers"); }
            set { }
        }
    }
}
