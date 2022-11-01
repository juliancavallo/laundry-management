using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Exceptions
{
    public class LoginAttemptsException : Exception
    {
        public LoginAttemptsException() { }

        public LoginAttemptsException(string message, Exception innerException) : base(message, innerException) { } 
     
        public LoginAttemptsException(string message) : base(message) { }
    }
}
