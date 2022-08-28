using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.DTOs;
using LaundryManagement.Interfaces.Domain.Entities;
using LaundryManagement.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class LocationDTO : ILocationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public string Address { get; set; }
        public ILocationDTO? ParentLocation { get; set; }
        public ILocationType LocationType { get; set; }
        public string CompleteName { get; set; }

        public bool IsChild(object location) =>
            this.ParentLocation != null && this.ParentLocation.Equals(location);

        public override bool Equals(object? obj)
        {
            return (obj is Location entity && Id == entity.Id) || (obj is LocationDTO dTO && Id == dTO.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
