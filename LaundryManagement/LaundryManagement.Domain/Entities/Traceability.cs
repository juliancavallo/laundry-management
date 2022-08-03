using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Traceability
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public DateTime Date { get; set; }
        public ItemStatus ItemStatus { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
        public MovementType MovementType { get; set; }
        public User User { get; set; }
    }
}
