using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class TraceabilityViewDTO
    {
        public string ItemCode { get; set; }
        public DateTime Date { get; set; }
        public string ItemStatus { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string Movement { get; set; }
        public string User { get; set; }
    }
}
