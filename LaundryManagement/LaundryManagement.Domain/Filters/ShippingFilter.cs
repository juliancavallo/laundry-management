using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class ShippingFilter
    {
        public ShippingTypeEnum? ShippingType { get; set; }
        public ShippingStatusEnum? ShippingStatus { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public LocationDTO? Origin { get; set; }
        public LocationDTO? Destination { get; set; }
        public List<int>? ShippingIds { get; set; }
    }
}
