using LaundryManagement.Interfaces.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.Entities
{
    public abstract class Component : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }

        public abstract IList<Component> Children { get;}
        public abstract void AddChildren(Component component);
        public abstract void RemoveChildren(Component component);
    }
}
