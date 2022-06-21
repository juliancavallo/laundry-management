using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.Entities
{
    public interface ITag
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
