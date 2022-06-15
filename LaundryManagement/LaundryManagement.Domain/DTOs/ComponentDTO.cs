using LaundryManagement.Interfaces.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public abstract class ComponentDTO : IComponentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }

        public abstract IList<IComponentDTO> Children { get; }
        public abstract void AddChildren(IComponentDTO dto);
        public abstract void RemoveChildren(IComponentDTO dto);
    }
}
