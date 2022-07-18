using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class ItemTypeMapper
    {
        public ItemType MapToEntity(ItemTypeDTO dto)
        {
            return new ItemType()
            {
                Id = dto.Id,
                Name = dto.Name,    
                Category = new Category()
                {
                    Id = dto.Category.Id,
                    Name = dto.Category.Name
                }
            };
        }

        public ItemTypeDTO MapToDTO(ItemType entity)
        {
            return new ItemTypeDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Category = new CategoryDTO()
                {
                    Id = entity.Category.Id,
                    Name = entity.Category.Name
                }
            };
        }
    }
}
