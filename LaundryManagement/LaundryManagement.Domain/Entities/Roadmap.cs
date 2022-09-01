using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Roadmap : Process
    {
        public int Id { get; set; }
        public List<Shipping> Shippings { get; set; }
        public RoadmapStatus Status { get; set; }
    }
}
