using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public abstract class ComponentDTO
    {
        public string Name { get; set; }
        public string Permission { get; set; }

        public abstract IList<ComponentDTO> Children { get; }
        public abstract void AddChildren(ComponentDTO dto);
        public abstract void RemoveChildren(ComponentDTO dto);
    }
}
