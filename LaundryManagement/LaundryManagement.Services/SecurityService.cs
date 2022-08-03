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
        public bool CheckPasswordSecurity(string password)
        {
            var policies = Session.Settings.PasswordPolicy;
            if(password.Length < policies.PasswordMinLength)
                return false;

            if(password.Count(x => char.IsUpper(x)) < policies.PasswordMinUppercase)
                return false;

            if (password.Count(x => char.IsLower(x)) < policies.PasswordMinLowercase)
                return false;

            if (password.Count(x => char.IsDigit(x)) < policies.PasswordMinNumbers)
                return false;

            if (password.Count(x => !char.IsLetterOrDigit(x) && !char.IsWhiteSpace(x)) < policies.PasswordMinSpecialCharacters)
                return false;

            return true;
        }
    }
}
