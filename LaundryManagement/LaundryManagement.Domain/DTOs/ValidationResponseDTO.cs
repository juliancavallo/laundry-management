using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class ValidationResponseDTO
    {
        public ValidationResponseDTO()
        {
            Messages = new List<string>();
        }

        public List<string> Messages { get; set; }
    }
}
