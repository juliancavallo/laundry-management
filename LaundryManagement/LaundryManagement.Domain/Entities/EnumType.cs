using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public abstract class EnumType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
