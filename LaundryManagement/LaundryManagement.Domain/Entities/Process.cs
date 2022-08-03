using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public abstract class Process
    {
        public DateTime CreatedDate { get; set; }
        public User CreationUser { get; set; }
        public Location Origin { get; set; }
        public Location Destination { get; set; }
    }
}
