using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public class Location : IEntity
    {
        public string Name { get; set; }
        public bool IsInternal { get; set; }
        public string Address { get; set; }
        public Location? ParentLocation { get; set; }
        public LocationType LocationType { get; set; }
        public string CompleteName
        {
            get
            {
                return GetParentName(ParentLocation) + Name;
            }
        }


        private string GetParentName(Location parent)
        {
            if (parent == null)
                return "";

            return parent.Name + " - " + GetParentName(parent.ParentLocation);
        }
    }
}
