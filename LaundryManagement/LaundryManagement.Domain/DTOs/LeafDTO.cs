
using LaundryManagement.Interfaces.Domain.DTOs;
using System;
using System.Collections.Generic;

namespace LaundryManagement.Domain.DTOs
{
    public class LeafDTO : ComponentDTO
    {
        public override IList<IComponentDTO> Children
        {
            get { throw new NotImplementedException(); }
        }
        public override void AddChildren(IComponentDTO dto) => throw new NotImplementedException();
        public override void RemoveChildren(IComponentDTO dto) => throw new NotImplementedException();
    }
}
