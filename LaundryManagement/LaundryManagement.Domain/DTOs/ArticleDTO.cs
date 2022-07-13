using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemTypeDTO Type { get; set; }
        public ColorDTO Color { get; set; }
        public SizeDTO Size { get; set; }
        public int Washes { get; set; }
    }
}
