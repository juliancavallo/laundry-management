using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface IPasswordPolicies
    {
        public int MinLength { get; set; }
        public int MinSpecialCharacters { get; set; }
        public int MinUppercase { get; set; }
        public int MinLowercase { get; set; }
        public int MinNumbers { get; set; }
    }
}
