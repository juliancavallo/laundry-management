using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Services
{
    public class SecurityService
    {
        public bool CheckPasswordSecurity(string password, IPasswordPolicies policies)
        {
            if(password.Length < policies.MinLength)
                return false;

            if(password.Count(x => char.IsUpper(x)) < policies.MinUppercase)
                return false;

            if (password.Count(x => char.IsLower(x)) < policies.MinLowercase)
                return false;

            if (password.Count(x => char.IsDigit(x)) < policies.MinNumbers)
                return false;

            if (password.Count(x => !char.IsLetterOrDigit(x) && !char.IsWhiteSpace(x)) < policies.MinSpecialCharacters)
                return false;

            return true;
        }
    }
}
