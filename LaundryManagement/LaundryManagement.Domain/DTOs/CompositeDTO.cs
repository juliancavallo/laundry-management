using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.Domain.DTOs
{
    public class CompositeDTO : ComponentDTO
    {
        private IList<ComponentDTO> _children;

        public CompositeDTO()
        {
            _children = new List<ComponentDTO>();
        }
        public override IList<ComponentDTO> Children
        {
            get { return _children; }
        }

        public override void AddChildren(ComponentDTO dto) => _children.Add(dto);
        
        public override void RemoveChildren(ComponentDTO dto) => _children.Remove(dto);
    }
}
