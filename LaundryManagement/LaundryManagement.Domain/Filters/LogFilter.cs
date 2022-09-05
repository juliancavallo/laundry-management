using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class LogFilter
    {
        public string Message { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public MovementTypeEnum? MovementType { get; set; }
    }
}
