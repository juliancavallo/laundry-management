using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationType ValidationType { get; set; }
        public ValidationException() { }

        public ValidationException(string message, ValidationType type, Exception innerException) : base(message, innerException) 
        {
            this.ValidationType = type;
        }

        public ValidationException(string message, ValidationType type) : base(message) 
        {
            this.ValidationType = type;
        }
    }
}
