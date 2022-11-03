using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class RoadmapFilter
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int? IdLocationOrigin { get; set; }
        public int? IdLocationDestination { get; set; }
        public RoadmapStatusEnum? Status { get; set; }
    }
}
