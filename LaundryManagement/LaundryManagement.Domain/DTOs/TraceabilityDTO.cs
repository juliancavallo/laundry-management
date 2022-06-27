using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class TraceabilityDTO
    {
        public int Id { get; set; }
        public ItemDTO Item { get; set; }
        public DateTime Date { get; set; }
        public ItemStatusEnum ItemStatus { get; set; }
        public string ItemStatusName { get; set; }
        public LocationDTO Origin { get; set; }
        public LocationDTO Destination { get; set; }
        public MovementTypeEnum MovementType { get; set; }
        public string MovementTypeName { get; set; }
        public UserDTO User { get; set; }
    }
}
