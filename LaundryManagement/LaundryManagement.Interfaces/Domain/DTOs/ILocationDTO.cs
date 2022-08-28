using LaundryManagement.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Interfaces.Domain.DTOs
{
    public interface ILocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public string Address { get; set; }
        public ILocationDTO? ParentLocation { get; set; } 
        public ILocationType LocationType { get; set; }
        public string CompleteName { get; set; }

        public bool IsChild(object location);
        public bool Equals(object? obj);
        public int GetHashCode();
    }
}
