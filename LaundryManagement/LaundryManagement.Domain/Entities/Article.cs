using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Article : IEntity
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
    }
}
