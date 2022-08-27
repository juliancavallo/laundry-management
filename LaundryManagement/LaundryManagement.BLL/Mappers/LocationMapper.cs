using LaundryManagement.Domain.DTOs;
using LaundryManagement.Domain.Entities;
using LaundryManagement.Domain.Enums;
using LaundryManagement.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaundryManagement.BLL.Mappers
{
    public class LocationMapper
    {
        public Location MapToEntity(LocationDTO dto)
        {
            return new Location()
            {
                Id = dto.Id,
                LocationType = (LocationType)dto.LocationType,
                IsInternal = dto.IsInternal,
                Address = dto.Address,
                Name = dto.Name
            };

        }

        public LocationDTO MapToDTO(Location entity)
        {
            var result = new LocationDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                IsInternal = entity.IsInternal,
                LocationType = (ILocationType)entity.LocationType,
                CompleteName = entity.CompleteName,
                ParentLocation = entity.ParentLocation != null ? this.MapToDTO(entity.ParentLocation) : null
            };
            return result;
        }
    }
}
