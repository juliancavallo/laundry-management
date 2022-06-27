using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Item : IEntity
    {
        public string Code { get; set; }
        public DateTime Created { get; set; }
        public Article Article { get; set; }
        public ItemStatus ItemStatus { get; set; }
    }
}
