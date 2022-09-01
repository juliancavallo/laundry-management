using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class RoadmapShippingViewDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string CreatedDate { get; set; }
        public bool Selected { get; set; }
    }
}
