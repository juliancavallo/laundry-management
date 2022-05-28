using LaundryManagement.Interfaces.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class CompositeDTO : ComponentDTO
    {
        private IList<IComponentDTO> _children;

        public CompositeDTO()
        {
            _children = new List<IComponentDTO>();
        }
        public override IList<IComponentDTO> Children
        {
            get { return _children; }
        }

        public override void AddChildren(IComponentDTO dto) => _children.Add(dto);
        
        public override void RemoveChildren(IComponentDTO dto) => _children.Remove(dto);
    }
}
