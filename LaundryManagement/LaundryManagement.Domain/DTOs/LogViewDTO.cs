using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class LogViewDTO
    {
        public DateTime Date { get; set; }
        public string Movement { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
    }
}
