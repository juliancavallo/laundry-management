using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class ItemViewDTO
    {
        public string Code { get; set; }
        public string Article { get; set; }
        public string ItemType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int Washes { get; set; }
    }
}
