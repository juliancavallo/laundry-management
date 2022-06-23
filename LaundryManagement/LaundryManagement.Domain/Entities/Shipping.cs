using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Shipping : IEntity
    {
        public DateTime CreatedDate { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public ShippingStatus Status { get; set; }
        public List<ShippingDetail> ShippingDetail { get; set; }
        public ShippingType Type { get; set; }
        public User User { get; set; }
    }
}
