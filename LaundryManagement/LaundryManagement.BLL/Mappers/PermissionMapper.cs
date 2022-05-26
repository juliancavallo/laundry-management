using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class PermissionMapper
    {
        public ComponentDTO MapToDTO(Component component)
        {
            ComponentDTO result;
            if(component.Permission == null)
            {
                result = new CompositeDTO();
                result.Name = component.Name;
                foreach(var item in component.Children)
                {
                    result.AddChildren(MapToDTO(item));
                }
            }
            else
            {
                result = new LeafDTO();
                result.Name = component.Name;
                result.Permission = component.Permission;
            }
            return result;
        }
    }
}
