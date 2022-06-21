using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class ItemTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
