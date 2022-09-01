using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class RoadmapDTO
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public LocationDTO Origin { get; set; }
        public LocationDTO Destination { get; set; }
        public RoadmapStatusEnum Status { get; set; }
        public string StatusName { get; set; }
        public List<ShippingDTO> Shippings { get; set; }
        public UserDTO CreationUser { get; set; }
    }
}
