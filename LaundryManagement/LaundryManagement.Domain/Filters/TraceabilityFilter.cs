using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class TraceabilityFilter
    {
        public string Code { get; set; }
        public MovementTypeEnum? MovementType { get; set; }
        public ItemStatusEnum? ItemStatus { get; set; }
    }
}
