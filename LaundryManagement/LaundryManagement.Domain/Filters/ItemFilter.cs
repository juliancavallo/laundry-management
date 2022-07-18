using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Filters
{
    public class ItemFilter
    {
        public string Code { get; set; }
        public ItemStatusEnum? ItemStatus { get; set; }
        public LocationDTO LocationDTO { get; set; }
        public ItemTypeDTO ItemType { get; set; }
    }
}
