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
            if(component is Composite)
            {
                result = new CompositeDTO();
                result.Name = component.Name;
                result.Id = component.Id;
                foreach(var item in component.Children)
                {
                    result.AddChildren(MapToDTO(item));
                }
            }
            else
            {
                result = new LeafDTO();
                result.Name = component.Name;
                result.Id = component.Id;
                result.Permission = component.Permission;
            }
            return result;
        }

        public Component MapToEntity(ComponentDTO dto)
        {
            Component result;
            if (dto is CompositeDTO)
            {
                result = new Composite();
                result.Name = dto.Name;
                result.Id = dto.Id;
                foreach (var item in dto.Children)
                {
                    result.AddChildren(MapToEntity(item as ComponentDTO));
                }
            }
            else
            {
                result = new Leaf();
                result.Name = dto.Name;
                result.Id = dto.Id;
                result.Permission = dto.Permission;
            }
            return result;
        }
    }
}
